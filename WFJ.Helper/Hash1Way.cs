using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace WFJ.Helper
{
    public class Hash1Way
    {
        public static string GetSalt(int len)
        {
            // Function takes a given length x and generates a random hex value of x digits.
            // Salt can be used to help protect passwords.  When a password is first stored in a
            // database generate a salt value also.  Concatenate the salt value with the password, 
            // and then encrypt it using the HashEncode function below.  Store both the salt value,
            // and the encrypted value in the database.  When a password needs to be verified, take 
            // the password concatenate the salt from the database.  Encode it using the HashEncode 
            // function below.  If the result matches the the encrypted password stored in the
            // database, then it is a match.  If not then the password is invalid.
            // 
            // 
            // Note: Passwords become case sensitive when using this encryption.
            // For more information on Password HASH Encoding, and SALT visit: http://local.15seconds.com/issue/000217.htm
            // 
            // Call this function if you wish to generate a random hex value of any given length
            // 
            // Written By: Mark G. Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact

            string salt = "";
            int intIndex = 0;
            int intRand = 0;

            if (!Information.IsNumeric(len))
            {
                return "00000000";
            }
            else if (System.Convert.ToDouble(len) != System.Convert.ToDouble(len) | System.Convert.ToInt32(len) < 1)
            {
                return "00000000";
            }

            VBMath.Randomize();

            for (intIndex = 1; intIndex <= (int)len; intIndex++)
            {
                intRand = System.Convert.ToInt32(VBMath.Rnd() * 1000) % 16;
                salt = salt + getDecHex(Convert.ToString(intRand));
            }

            return salt;
        }
        public static string getDecHex(string strHex)
        {
            // Function Converts a single decimal value(0 - 15) into it's hex equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string decHex = "";
            switch (System.Convert.ToInt32(strHex))
            {
                case 0:
                    {
                        decHex = "0";
                        break;
                    }

                case 1:
                    {
                        decHex = "1";
                        break;
                    }

                case 2:
                    {
                        decHex = "2";
                        break;
                    }

                case 3:
                    {
                        decHex = "3";
                        break;
                    }

                case 4:
                    {
                        decHex = "4";
                        break;
                    }

                case 5:
                    {
                        decHex = "5";
                        break;
                    }

                case 6:
                    {
                        decHex = "6";
                        break;
                    }

                case 7:
                    {
                        decHex = "7";
                        break;
                    }

                case 8:
                    {
                        decHex = "8";
                        break;
                    }

                case 9:
                    {
                        decHex = "9";
                        break;
                    }

                case 10:
                    {
                        decHex = "A";
                        break;
                    }

                case 11:
                    {
                        decHex = "B";
                        break;
                    }

                case 12:
                    {
                        decHex = "C";
                        break;
                    }

                case 13:
                    {
                        decHex = "D";
                        break;
                    }

                case 14:
                    {
                        decHex = "E";
                        break;
                    }

                case 15:
                    {
                        decHex = "F";
                        break;
                    }

                default:
                    {
                        decHex = "Z";
                        break;
                    }
            }
            return decHex;
        }

        public static string HashEncode(string strSecret)
        {
            string strEncode = "";
            string hashEncode = "";
            // Function takes an ASCII string less than 2^61 characters long and 
            // one way hash encrypts it using 160 bit encryption into a 40 digit hex value.
            // The encoded hex value cannot be decoded to the original string value.
            // 
            // This is the only function that you need to call for encryption.
            // 
            // Written By: Mark G. Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            // The author makes no warranties as to the validity, and/or authenticity of this code.
            // You may use any code found herein at your own risk.
            // This code was written to follow as closely as possible the standards found in
            // Federal Information Processing Standards Publication (FIPS PUB 180-1)
            // http://csrc.nist.gov/fips/fip180-1.txt -- Secure Hash Standard SHA-1
            // 
            // This code is for private use only, and the security and/or encryption of the resulting
            // hexadecimal value is not warrented or gaurenteed in any way.
            // 
            string[] strH = new string[5];
            var intPos = 0;


            if (Strings.Len(strSecret) == 0 | Strings.Len(strSecret) >= Math.Pow(2, 61))
            {
                hashEncode = "0000000000000000000000000000000000000000";
                return hashEncode;
            }


            // Initial Hex words are used for encoding Digest.  
            // These can be any valid 8-digit hex value (0 to F)
            strH[0] = "FB0C14C2";
            strH[1] = "9F00AB2E";
            strH[2] = "991FFA67";
            strH[3] = "76FA2C3F";
            strH[4] = "ADE426FA";

            for (intPos = 1; intPos <= Strings.Len(strSecret); intPos += 56)
            {
                strEncode = Strings.Mid(Convert.ToString(strSecret), intPos, 56); // get 56 character chunks
                strEncode = WordToBinary(strEncode); // convert to binary
                strEncode = PadBinary(strEncode); // make it 512 bites
                strEncode = BlockToHex(strEncode); // convert to hex value

                // Encode the hex value using the previous runs digest
                // If it is the first run then use the initial values above
                strEncode = DigestHex(strEncode, strH[0], strH[1], strH[2], strH[3], strH[4]);

                // Combine the old digest with the new digest
                strH[0] = HexAdd(Strings.Left(strEncode, 8), strH[0]);
                strH[1] = HexAdd(Strings.Mid(strEncode, 9, 8), strH[1]);
                strH[2] = HexAdd(Strings.Mid(strEncode, 17, 8), strH[2]);
                strH[3] = HexAdd(Strings.Mid(strEncode, 25, 8), strH[3]);
                strH[4] = HexAdd(Strings.Right(strEncode, 8), strH[4]);
            }

            // This is the final Hex Digest
            return strH[0] + strH[1] + strH[2] + strH[3] + strH[4];
        }

        public static string WordToBinary(string strWord)
        {
            // Function Converts a 8 digit hex value into it's 32 bit binary equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header kept intact
            // 
            string strTemp;
            string strBinary = "";
            int intPos;

            for (intPos = 1; intPos <= Strings.Len(strWord); intPos++)
            {
                strTemp = Strings.Mid(strWord, System.Convert.ToInt32(intPos), 1);
                strBinary = strBinary + IntToBinary(Asc(Convert.ToChar(strTemp)));
            }
            return strBinary;
        }

        public static int Asc(char String)
        {
            int num;
            byte[] numArray;
            int num1 = Convert.ToInt32(String);
            if (num1 >= 128)
            {
                try
                {
                    Encoding fileIOEncoding = Encoding.Default;
                    char[] str = new char[] { String };
                    if (!fileIOEncoding.IsSingleByte)
                    {
                        numArray = new byte[2];
                        if (fileIOEncoding.GetBytes(str, 0, 1, numArray, 0) != 1)
                        {
                            if (BitConverter.IsLittleEndian)
                            {
                                byte num2 = numArray[0];
                                numArray[0] = numArray[1];
                                numArray[1] = num2;
                            }
                            num = BitConverter.ToInt16(numArray, 0);
                        }
                        else
                        {
                            num = numArray[0];
                        }
                    }
                    else
                    {
                        numArray = new byte[1];
                        fileIOEncoding.GetBytes(str, 0, 1, numArray, 0);
                        num = numArray[0];
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            else
            {
                num = num1;
            }
            return num;
        }

        public static string IntToBinary(int intNum)
        {

            // Function Converts an integer number to it's binary equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string intToBinary = "";
            string strBinary = "";
            string strTemp = "";
            string strMyBin = "";
            int intNew = 0;
            int intTemp = 0;
            double dblNew;

            intNew = intNum;

            while (intNew > 1)
            {
                dblNew = System.Convert.ToDouble(intNew) / 2;
                intNew = Convert.ToInt32(Math.Round(System.Convert.ToDouble(dblNew) - 0.1, 0));
                if (System.Convert.ToDouble(dblNew) == System.Convert.ToDouble(intNew))
                    strBinary = "0" + strBinary;
                else
                    strBinary = "1" + strBinary;
            }

            strBinary = intNew + strBinary;

            intTemp = Strings.Len(strBinary) % 8;

            for (intNew = intTemp; intNew <= 7; intNew++)
                strBinary = "0" + strBinary;

            strMyBin += Convert.ToString(System.Convert.ToInt64(intNum), 2).PadLeft(8, '0');

            intToBinary = strMyBin;
            return intToBinary;
        }
        public static string PadBinary(string strBinary)
        {

            // Function adds 0's to a binary string until it reaches 448 bits.
            // The lenghth of the original string is incoded into the last 16 bits.
            // The end result is a binary string 512 bits long
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 

            int intPos = 0;
            int intLen = 0;
            string strTemp = "";

            intLen = Strings.Len(strBinary);

            strBinary = strBinary + "1";

            for (intPos = Strings.Len(strBinary); intPos <= 447; intPos++)
                strBinary = strBinary + "0";

            strTemp = IntToBinary(intLen);

            for (intPos = Strings.Len(strTemp); intPos <= 63; intPos++)
                strTemp = "0" + strTemp;

            strBinary = strBinary + strTemp;

            return strBinary;
        }
        public static string BlockToHex(string strBinary)
        {

            // Function Converts a 32 bit binary string into it's 8 digit hex equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            int intPos = 0;
            string strHex = "";

            for (intPos = 1; intPos <= Strings.Len(strBinary); intPos += 4)
                strHex = strHex + BinaryToHex(Strings.Mid(strBinary, intPos, 4));

            return strHex;
        }
        public static string BinaryToHex(string strBinary)
        {

            // Function Converts a 4 bit binary value into it's hex equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string binaryToHex = "";
            switch (strBinary)
            {
                case "0000":
                    {
                        binaryToHex = "0";
                        break;
                    }

                case "0001":
                    {
                        binaryToHex = "1";
                        break;
                    }

                case "0010":
                    {
                        binaryToHex = "2";
                        break;
                    }

                case "0011":
                    {
                        binaryToHex = "3";
                        break;
                    }

                case "0100":
                    {
                        binaryToHex = "4";
                        break;
                    }

                case "0101":
                    {
                        binaryToHex = "5";
                        break;
                    }

                case "0110":
                    {
                        binaryToHex = "6";
                        break;
                    }

                case "0111":
                    {
                        binaryToHex = "7";
                        break;
                    }

                case "1000":
                    {
                        binaryToHex = "8";
                        break;
                    }

                case "1001":
                    {
                        binaryToHex = "9";
                        break;
                    }

                case "1010":
                    {
                        binaryToHex = "A";
                        break;
                    }

                case "1011":
                    {
                        binaryToHex = "B";
                        break;
                    }

                case "1100":
                    {
                        binaryToHex = "C";
                        break;
                    }

                case "1101":
                    {
                        binaryToHex = "D";
                        break;
                    }

                case "1110":
                    {
                        binaryToHex = "E";
                        break;
                    }

                case "1111":
                    {
                        binaryToHex = "F";
                        break;
                    }

                default:
                    {
                        binaryToHex = "Z";
                        break;
                    }
            }
            return binaryToHex;
        }


        public static string DigestHex(string strHex, string strH0, string strH1, string strH2, string strH3, string strH4)
        {
            string[] strWords = new string[80], adoConst = new string[5];

            // Main encoding function.  Takes a 128 digit/512 bit hex value and one way encrypts it into
            // a 40 digit/160 bit hex value.
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string strTemp = "";
            string strTemp1 = "";
            string strTemp2 = "";
            string strTemp3 = "";
            string strTemp4 = "";
            int intPos;
            string[] strH = new string[5], strA = new string[5], strK = new string[4];

            // Constant hex words are used for encryption, these can be any valid 8 digit hex value
            strK[0] = "5A827999";
            strK[1] = "6ED9EBA1";
            strK[2] = "8F1BBCDC";
            strK[3] = "CA62C1D6";

            // Hex words are used in the encryption process, these can be any valid 8 digit hex value
            strH[0] = strH0;
            strH[1] = strH1;
            strH[2] = strH2;
            strH[3] = strH3;
            strH[4] = strH4;

            // divide the Hex block into 16 hex words
            for (intPos = 0; intPos <= (Strings.Len(strHex) / (double)8) - 1; intPos++)
                strWords[System.Convert.ToInt32(intPos)] = Strings.Mid(strHex, (System.Convert.ToInt32(intPos) * 8) + 1, 8);


            // encode the Hex words using the constants above
            // innitialize 80 hex word positions
            for (intPos = 16; intPos <= 79; intPos++)
            {
                strTemp = strWords[System.Convert.ToInt32(intPos) - 3];
                strTemp1 = HexBlockToBinary(strTemp);
                strTemp = strWords[System.Convert.ToInt32(intPos) - 8];
                strTemp2 = HexBlockToBinary(strTemp);
                strTemp = strWords[System.Convert.ToInt32(intPos) - 14];
                strTemp3 = HexBlockToBinary(strTemp);
                strTemp = strWords[System.Convert.ToInt32(intPos) - 16];
                strTemp4 = HexBlockToBinary(strTemp);
                strTemp = BinaryXOR(strTemp1, strTemp2);
                strTemp = BinaryXOR(strTemp, strTemp3);
                strTemp = BinaryXOR(strTemp, strTemp4);
                strWords[System.Convert.ToInt32(intPos)] = BlockToHex(BinaryShift(strTemp, 1));
            }

            // initialize the changing word variables with the initial word variables
            strA[0] = strH[0];
            strA[1] = strH[1];
            strA[2] = strH[2];
            strA[3] = strH[3];
            strA[4] = strH[4];

            // Main encryption loop on all 80 hex word positions
            for (intPos = 0; intPos <= 79; intPos++)
            {
                strTemp = BinaryShift(HexBlockToBinary(strA[0]), 5);
                strTemp1 = HexBlockToBinary(strA[3]);
                strTemp2 = HexBlockToBinary(strWords[System.Convert.ToInt32(intPos)]);

                switch (intPos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                        {
                            strTemp3 = HexBlockToBinary(strK[0]);
                            strTemp4 = BinaryOR(BinaryAND(HexBlockToBinary(strA[1]), HexBlockToBinary(strA[2])), BinaryAND(BinaryNOT(HexBlockToBinary(strA[1])), HexBlockToBinary(strA[3])));
                            break;
                        }

                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                        {
                            strTemp3 = HexBlockToBinary(strK[1]);
                            strTemp4 = BinaryXOR(BinaryXOR(HexBlockToBinary(strA[1]), HexBlockToBinary(strA[2])), HexBlockToBinary(strA[3]));
                            break;
                        }

                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 47:
                    case 48:
                    case 49:
                    case 50:
                    case 51:
                    case 52:
                    case 53:
                    case 54:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                        {
                            strTemp3 = HexBlockToBinary(strK[2]);
                            strTemp4 = BinaryOR(BinaryOR(BinaryAND(HexBlockToBinary(strA[1]), HexBlockToBinary(strA[2])), BinaryAND(HexBlockToBinary(strA[1]), HexBlockToBinary(strA[3]))), BinaryAND(HexBlockToBinary(strA[2]), HexBlockToBinary(strA[3])));
                            break;
                        }

                    case 60:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                    case 67:
                    case 68:
                    case 69:
                    case 70:
                    case 71:
                    case 72:
                    case 73:
                    case 74:
                    case 75:
                    case 76:
                    case 77:
                    case 78:
                    case 79:
                        {
                            strTemp3 = HexBlockToBinary(strK[3]);
                            strTemp4 = BinaryXOR(BinaryXOR(HexBlockToBinary(strA[1]), HexBlockToBinary(strA[2])), HexBlockToBinary(strA[3]));
                            break;
                        }
                }

                strTemp = BlockToHex(strTemp);
                strTemp1 = BlockToHex(strTemp1);
                strTemp2 = BlockToHex(strTemp2);
                strTemp3 = BlockToHex(strTemp3);
                strTemp4 = BlockToHex(strTemp4);

                strTemp = HexAdd(strTemp, strTemp1);
                strTemp = HexAdd(strTemp, strTemp2);
                strTemp = HexAdd(strTemp, strTemp3);
                strTemp = HexAdd(strTemp, strTemp4);

                strA[4] = strA[3];
                strA[3] = strA[2];
                strA[2] = BlockToHex(BinaryShift(HexBlockToBinary(strA[1]), 30));
                strA[1] = strA[0];
                strA[0] = strTemp;
            }

            // Concatenate the final Hex Digest
            return strA[0] + strA[1] + strA[2] + strA[3] + strA[4];
        }


        public static string HexBlockToBinary(string strHex)
        {
            // Function Converts a 8 digit/32 bit hex value to its 32 bit binary equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            int intPos = 0;
            string strTemp = "";

            for (intPos = 1; intPos <= Strings.Len(strHex); intPos++)
                strTemp = strTemp + HexToBinary(Strings.Mid(strHex, System.Convert.ToInt32(intPos), 1));

            return strTemp;
        }
        public static string HexToBinary(string btHex)
        {

            // Function Converts a single hex value into it's binary equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string hexToBinary = "";
            switch (btHex)
            {
                case "0":
                    {
                        hexToBinary = "0000";
                        break;
                    }

                case "1":
                    {
                        hexToBinary = "0001";
                        break;
                    }

                case "2":
                    {
                        hexToBinary = "0010";
                        break;
                    }

                case "3":
                    {
                        hexToBinary = "0011";
                        break;
                    }

                case "4":
                    {
                        hexToBinary = "0100";
                        break;
                    }

                case "5":
                    {
                        hexToBinary = "0101";
                        break;
                    }

                case "6":
                    {
                        hexToBinary = "0110";
                        break;
                    }

                case "7":
                    {
                        hexToBinary = "0111";
                        break;
                    }

                case "8":
                    {
                        hexToBinary = "1000";
                        break;
                    }

                case "9":
                    {
                        hexToBinary = "1001";
                        break;
                    }

                case "A":
                    {
                        hexToBinary = "1010";
                        break;
                    }

                case "B":
                    {
                        hexToBinary = "1011";
                        break;
                    }

                case "C":
                    {
                        hexToBinary = "1100";
                        break;
                    }

                case "D":
                    {
                        hexToBinary = "1101";
                        break;
                    }

                case "E":
                    {
                        hexToBinary = "1110";
                        break;
                    }

                case "F":
                    {
                        hexToBinary = "1111";
                        break;
                    }

                default:
                    {
                        hexToBinary = "2222";
                        break;
                    }
            }
            return hexToBinary;
        }

        public static string BinaryXOR(string strBin1, string strBin2)
        {
            // Function performs an exclusive or function on each position of two binary values
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string strBinaryFinal =string.Empty;
            int intPos = 0;

            for (intPos = 0; intPos < Strings.Len(strBin1); intPos++)
            {
                if (strBin1.Substring(intPos, 1) == strBin2.Substring(intPos, 1))
                {
                    strBinaryFinal = strBinaryFinal + "0";
                }
                else
                {
                    strBinaryFinal = strBinaryFinal + "1";
                }
            }

            return strBinaryFinal;
        }

        public static string BinaryShift(string strBinary, int intPos)
        {
            // Function circular left shifts a binary value n places
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            return Strings.Right(strBinary, Strings.Len(strBinary) - System.Convert.ToInt32(intPos)) + Strings.Left(strBinary, System.Convert.ToInt32(intPos));
        }

        public static string BinaryOR(string strBin1, string strBin2)
        {
            // Function performs an inclusive or function on each position of two binary values
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string strBinaryFinal=string.Empty;
            int intPos = 0;

            for (intPos = 1; intPos <= Strings.Len(strBin1); intPos++)
            {
                if (Strings.Mid(strBin1, System.Convert.ToInt32(intPos), 1) == "1" | Strings.Mid(strBin2, System.Convert.ToInt32(intPos), 1) == "1")
                    strBinaryFinal = strBinaryFinal + "1";
                else
                    strBinaryFinal = strBinaryFinal + "0";
            }

            return strBinaryFinal;
        }

        public static string BinaryAND(string strBin1, string strBin2)
        {
            // Function performs an AND function on each position of two binary values
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string strBinaryFinal = string.Empty;
            int intPos = 0;

            for (intPos = 1; intPos <= Strings.Len(strBin1); intPos++)
            {
                if (Strings.Mid(strBin1, System.Convert.ToInt32(intPos), 1) == "1" & Strings.Mid(strBin2, System.Convert.ToInt32(intPos), 1) == "1")
                    strBinaryFinal = strBinaryFinal + "1";
                else
                    strBinaryFinal = strBinaryFinal + "0";
            }

            return strBinaryFinal;
        }

        public static string HexAdd(string strHex1, string strHex2)
        {
            // Function adds to 8 digit/32 bit hex values together Mod 2^32
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            double intCalc;
            string strNew;

            intCalc = 0;
            intCalc = (double)System.Convert.ToDouble(HexToInt(strHex1)) + System.Convert.ToDouble(HexToInt(strHex2));
            while (System.Convert.ToDouble(intCalc) > Math.Pow(2, 32))
                intCalc = System.Convert.ToDouble(intCalc) - Math.Pow(2, 32);

            strNew = IntToBinary(System.Convert.ToInt32(intCalc));
            while (Strings.Len(strNew) < 32)
                strNew = "0" + strNew;
            strNew = BlockToHex(strNew);

            if (Strings.InStr(strNew, "00") == 1 & Strings.Len(strNew) == 10)
                strNew = Strings.Right(strNew, 8);

            return strNew;
        }

        public static double HexToInt(string strHex)
        {
            // Function Converts a hex word to its base 10(decimal) equivalent
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 

            double intNew = 0;
            double intPos = 0;
            double intLen;

            intNew = 0;
            intLen = System.Convert.ToDouble(Strings.Len(strHex)) - 1;

            for (intPos = (double)intLen; intPos >= 0; intPos += -1)
            {
                switch (Strings.Mid(strHex, System.Convert.ToInt32(intPos) + 1, 1))
                {
                    case "0":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (0 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "1":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (1 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "2":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (2 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "3":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (3 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "4":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (4 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "5":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (5 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "6":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (6 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "7":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (7 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "8":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (8 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "9":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (9 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "A":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (10 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "B":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (11 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "C":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (12 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "D":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (13 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "E":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (14 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }

                    case "F":
                        {
                            intNew = System.Convert.ToDouble(intNew) + (15 * Math.Pow(16, System.Convert.ToDouble(intLen - intPos)));
                            break;
                        }
                }
            }

            return  (double)intNew;
        }

        public static string BinaryNOT(string strBinary)
        {
            // Function makes each position of a binary value from 1 to 0 and 0 to 1
            // 
            // Written By: Mark Jager
            // Written Date: 8/10/2000
            // 
            // Free to distribute as long as code is not modified, and header is kept intact
            // 
            string strBinaryFinal=string.Empty;
            int intPos;

            for (intPos = 1; intPos <= Strings.Len(strBinary); intPos++)
            {
                if (Strings.Mid(strBinary, System.Convert.ToInt32(intPos), 1) == "1")
                    strBinaryFinal = strBinaryFinal + "0";
                else
                    strBinaryFinal = strBinaryFinal + "1";
            }

            return strBinaryFinal;
        }


        //public static string ToAbsoluteUrl(this string relativeUrl) //Use absolute URL instead of adding phycal path for CSS, JS and Images     
        //{
        //    if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
        //    if (HttpContext.Current == null) return relativeUrl;
        //    if (relativeUrl.StartsWith("/")) relativeUrl = relativeUrl.Insert(0, "~");
        //    if (!relativeUrl.StartsWith("~/")) relativeUrl = relativeUrl.Insert(0, "~/");
        //    var url = HttpContext.Current.Request.Url;
        //    var port = url.Port != 80 ? (":" + url.Port) : String.Empty;
        //    return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        //}
        //public static string GeneratePassword(int length) //length of salt    
        //{
        //    const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        //    var randNum = new Random();
        //    var chars = new char[length];
        //    var allowedCharCount = allowedChars.Length;
        //    for (var i = 0; i <= length - 1; i++)
        //    {
        //        chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
        //    }
        //    return new string(chars);
        //}
        //public static string EncodePassword(string pass, string salt) //encrypt password    
        //{
        //    byte[] bytes = Encoding.Unicode.GetBytes(pass);
        //    byte[] src = Encoding.Unicode.GetBytes(salt);
        //    byte[] dst = new byte[src.Length + bytes.Length];
        //    System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        //    System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
        //    HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
        //    byte[] inArray = algorithm.ComputeHash(dst);
        //    //return Convert.ToBase64String(inArray);    
        //    return EncodePasswordMd5(Convert.ToBase64String(inArray));
        //}
        //public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        //{
        //    Byte[] originalBytes;
        //    Byte[] encodedBytes;
        //    MD5 md5;
        //    //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
        //    md5 = new MD5CryptoServiceProvider();
        //    originalBytes = ASCIIEncoding.Default.GetBytes(pass);
        //    encodedBytes = md5.ComputeHash(originalBytes);
        //    //Convert encoded bytes back to a 'readable' string    
        //    return BitConverter.ToString(encodedBytes);
        //}
        //public static string base64Encode(string sData) // Encode    
        //{
        //    try
        //    {
        //        byte[] encData_byte = new byte[sData.Length];
        //        encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
        //        string encodedData = Convert.ToBase64String(encData_byte);
        //        return encodedData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in base64Encode" + ex.Message);
        //    }
        //}
        //public static string base64Decode(string sData) //Decode    
        //{
        //    try
        //    {
        //        var encoder = new System.Text.UTF8Encoding();
        //        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //        byte[] todecodeByte = Convert.FromBase64String(sData);
        //        int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
        //        char[] decodedChar = new char[charCount];
        //        utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
        //        string result = new String(decodedChar);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in base64Decode" + ex.Message);
        //    }
        //}
        //private static string CreateSalt(int size)
        //{
        //    //Generate a cryptographic random number.
        //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //    byte[] buff = new byte[size];
        //    rng.GetBytes(buff);

        //    // Return a Base64 string representation of the random number.
        //    return Convert.ToBase64String(buff);
        //}

        //private static string CreatePasswordHash(string pwd, string salt)
        //{
        //    string saltAndPwd = String.Concat(pwd, salt);
        //    string hashedPwd =
        //        FormsAuthentication.HashPasswordForStoringInConfigFile(
        //        saltAndPwd, "sha1");
        //    return hashedPwd;
        //}


    }
}
