using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Diagnostics;
using NAudio.Wave;

namespace EquationV2
{
    public static class FileHelper
    {
        public static Double[] PrepareWavForFft(String wavePath, out int SampleRate)
        {
            Double[] data;
            byte[] wave;
            byte[] sR = new byte[4];
            System.IO.FileStream WaveFile = System.IO.File.OpenRead(wavePath);
            wave = new byte[WaveFile.Length];
            data = new Double[(wave.Length - 44) / 4];//shifting the headers out of the PCM data;
            WaveFile.Read(wave, 0, Convert.ToInt32(WaveFile.Length));//read the wave file into the wave variable
            /***********Converting and PCM accounting***************/
            for (int i = 0; i < data.Length - i * 4; i++)
            {
                data[i] = (BitConverter.ToInt32(wave, (1 + i) * 4)) / 65536.0;
                //65536.0.0=2^n,       n=bits per sample;
            }
            /**************assigning sample rate**********************/
            for (int i = 24; i < 28; i++)
            {
                sR[i - 24] = wave[i];
            }
            SampleRate = BitConverter.ToInt32(sR, 0);
            return data;
        }
        public static bool ReadWav(string filename, out double[] L, out double[] R)
        {
            L = R = null;
            //float [] left = new float[1];
            //float [] right;
            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    int chunkID = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    int riffType = reader.ReadInt32();


                    // chunk 1
                    int fmtID = reader.ReadInt32();
                    int fmtSize = reader.ReadInt32(); // bytes for this chunk
                    int fmtCode = reader.ReadInt16();
                    int channels = reader.ReadInt16();
                    int sampleRate = reader.ReadInt32();
                    int byteRate = reader.ReadInt32();
                    int fmtBlockAlign = reader.ReadInt16();
                    int bitDepth = reader.ReadInt16();

                    if (fmtSize == 18)
                    {
                        // Read any extra values
                        int fmtExtraSize = reader.ReadInt16();
                        reader.ReadBytes(fmtExtraSize);
                    }

                    // chunk 2
                    int dataID = reader.ReadInt32();
                    int bytes = reader.ReadInt32();

                    // DATA!
                    byte[] byteArray = reader.ReadBytes(bytes);

                    int bytesForSamp = bitDepth / 8;
                    int samps = bytes / bytesForSamp;


                    double[] asFloat = null;
                    switch (bitDepth)
                    {
                        case 64:
                            double[]
                            asDouble = new double[samps];
                            Buffer.BlockCopy(byteArray, 0, asDouble, 0, bytes);
                            asFloat = Array.ConvertAll(asDouble, e => (double)e);
                            break;
                        case 32:
                            asFloat = new double[samps];
                            Buffer.BlockCopy(byteArray, 0, asFloat, 0, bytes);
                            break;
                        case 16:
                            Int16[]
                            asInt16 = new Int16[samps];
                            Buffer.BlockCopy(byteArray, 0, asInt16, 0, bytes);
                            asFloat = Array.ConvertAll(asInt16, e => e / (double)Int16.MaxValue);
                            break;
                        default:
                            return false;
                    }

                    switch (channels)
                    {
                        case 1:
                            L = asFloat;
                            R = null;
                            return true;
                        case 2:
                            L = new double[samps];
                            R = new double[samps];
                            for (int i = 0, s = 0; i < samps; i++)
                            {
                                L[i] = asFloat[s++];
                                R[i] = asFloat[s++];
                            }
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                return false;
                //left = new float[ 1 ]{ 0f };
            }
        }
        public static byte[] GetBytes(double[] values)
        {
            return values.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
        }
        public static byte[] GetBytesAlt(double[] values)
        {
            var result = new byte[values.Length * sizeof(double)];
            Buffer.BlockCopy(values, 0, result, 0, result.Length);
            return result;
        }
        public static double[] GetDoubles(byte[] bytes)
        {
            return Enumerable.Range(0, bytes.Length / sizeof(double))
                .Select(offset => BitConverter.ToDouble(bytes, offset * sizeof(double)))
                .ToArray();
        }
        public static double[] GetDoublesAlt(byte[] bytes)
        {
            var result = new double[bytes.Length / sizeof(double)];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }
        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.WriteData(buffer, 0, bytesRead);
                    }
                }
            }
        }
        // convert two bytes to one double in the range -1 to 1
        static double BytesToDouble(byte firstByte, byte secondByte)
        {
            // convert two bytes to one short (little endian)
            var s = (secondByte << 8) | firstByte;
            // convert to range from -1 to (just below) 1
            return s / 32768.0;
        }

        // Returns left and right double arrays. 'right' will be null if sound is mono.
        public static void OpenWav(string filename, out double[] left, out double[] right)
        {
            byte[] wav = File.ReadAllBytes(filename);

            // Determine if mono or stereo
            int channels = wav[22];     // Forget byte 23 as 99.999% of WAVs are 1 or 2 channels

            // Get past all the other sub chunks to get to the data subchunk:
            int pos = 12;   // First Subchunk ID from 12 to 16

            // Keep iterating until we find the data chunk (i.e. 64 61 74 61 ...... (i.e. 100 97 116 97 in decimal))
            while (!(wav[pos] == 100 && wav[pos + 1] == 97 && wav[pos + 2] == 116 && wav[pos + 3] == 97))
            {
                pos += 4;
                int chunkSize = wav[pos] + wav[pos + 1] * 256 + wav[pos + 2] * 65536 + wav[pos + 3] * 16777216;
                pos += 4 + chunkSize;
            }
            pos += 8;

            // Pos is now positioned to start of actual sound data.
            int samples = (wav.Length - pos) / 2;     // 2 bytes per sample (16 bit sound mono)
            if (channels == 2) samples /= 2;        // 4 bytes per sample (16 bit stereo)

            // Allocate memory (right will be null if only mono sound)
            left = new double[samples];
            if (channels == 2) right = new double[samples];
            else right = null;

            // Write to double array/s:
            int i = 0;
            while (pos < wav.Length-1)
            {
                left[i] = BytesToDouble(wav[pos], wav[pos + 1]);
                pos += 2;
                if (channels == 2)
                {
                    right[i] = BytesToDouble(wav[pos], wav[pos + 1]);
                    pos += 2;
                }
                i++;
            }
        }
    }
}
