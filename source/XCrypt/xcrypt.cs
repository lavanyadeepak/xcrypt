
// Thanks for Frank Fang & William M. Rawls 's example code for SymmetricAlgorithm.
// Thanks for Markus Hahn 's BelowFish source code.
// Thanks for Shaun Wilde 's ManyMonkey source code for TwoFish.
// (Modified the TransformFinalBlock() in TwofishEncryption.cs, the original code has not pad the finial block correctly)
 
using System;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Blowfish_NET;
using Twofish_NET;

namespace XCrypt
{
	public class XCryptEngine : IDisposable
	{
        private DateTime dtApplicationLaunchDateTime = DateTime.Now;

        /// <summary>
        ///   Supported Cryptogrphy Algorithms
        /// </summary>
        public enum AlgorithmType : int 
		{
            None,
            SHA,
            SHA256,
            SHA384,
            SHA512, 
            MD5,
			DES, 
            RC2, 
            Rijndael, 
            TripleDES, 
            BlowFish, 
            Twofish, 
            Blake256, 
            Blake512
		}

		private AlgorithmType _algorithm = AlgorithmType.None;


        /// <summary>
        ///   Desired Algorithm for this Instance
        /// </summary>
		public AlgorithmType Algorithm 
		{
			get{ return _algorithm;}
			set 
			{
				_algorithm = value;
				InitializeEngine();
			}
		}
		
		private bool _isHash;

        /// <summary>
        ///   Is the chosen algorith a hash one?
        /// </summary>
		public bool IsHashAlgorithm
		{
			get {return _isHash;}
			set {}
		}
		private bool _isSymmetric;

        /// <summary>
        ///    Is the chosen algorith a hash one?
        /// </summary>
		public bool IsSymmetricAlgorithm 
		{
			get {return _isSymmetric;}
			set {}
		}

		private string _key = null;

        /// <summary>
        ///   Encryption Key to use
        /// </summary>
		public string Key
		{
			get {return _key;}
			set {_key = this.formatKey(value);}
		}


		private HashAlgorithmEngine _he;
		private SymmetricAlgorithmEngine _se;

		private string formatKey(string k) 
		{
			if( k == null || k.Length == 0 ) return null;
			return k.Trim();
		}

        private string GetApplicationDefaultKey()
        {
            return (dtApplicationLaunchDateTime.ToString());
        }

		public XCryptEngine()
		{
			
		}

		public XCryptEngine(AlgorithmType al)
		{
			_algorithm = al;

			InitializeEngine();
		}

        public void Dispose()
        {
            try
            {
                DestroyEngine();
            }
            catch { }
        }

		public string Decrypt(string src) 
		{
			string result = "";

			if ( _isSymmetric ) 
			{
				if( _key == null )
					result = _se.Decrypting(src,GetApplicationDefaultKey());
				else result = _se.Decrypting(src,_key);
			}

			return result;
		}

		public string Decrypt(string src,string ckey) 
		{
			string result = "";

			if ( _isSymmetric ) 
			{
				result = _se.Decrypting(src,ckey);
			}

			return result;
		}

		public string Encrypt(string src) 
		{
			string result = "";

			if( _isHash ) 
			{
				result = _he.Encoding(src);
			} 
			else if ( _isSymmetric ) 
			{
				if( _key == null )
					result = _se.Encrypting(src,GetApplicationDefaultKey());
				else result = _se.Encrypting(src,_key);
			}

			return result;
		}

		public string Encrypt(string src,string ckey) 
		{
			string result = "";

			if( _isHash ) 
			{
				result = _he.Encoding(src);
			} 
			else if ( _isSymmetric ) 
			{
				result = _se.Encrypting(src,ckey);
			}

			return result;
		}

		public bool DestroyEngine() 
		{
			_se = null;
			_he = null;
			return true;
		}

		public bool InitializeEngine(AlgorithmType at) 
		{
			_algorithm = at; 

			return InitializeEngine();
		}

        public bool IsSymmetric(AlgorithmType enumAlgorithm)
        {
            if (_algorithm == AlgorithmType.BlowFish ||
                _algorithm == AlgorithmType.DES ||
                _algorithm == AlgorithmType.RC2 ||
                _algorithm == AlgorithmType.Rijndael ||
                _algorithm == AlgorithmType.TripleDES ||
                _algorithm == AlgorithmType.Twofish)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public bool IsHash(AlgorithmType enumAlgorithm)
        {
            if (_algorithm == AlgorithmType.MD5 ||
                _algorithm == AlgorithmType.SHA ||
                _algorithm == AlgorithmType.SHA256 ||
                _algorithm == AlgorithmType.SHA384 ||
                _algorithm == AlgorithmType.SHA512 ||
                _algorithm == AlgorithmType.Blake256 ||
                _algorithm == AlgorithmType.Blake512)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

		public bool InitializeEngine() 
		{
			if( _algorithm == AlgorithmType.None )
				return false;

            _isSymmetric = IsSymmetric(_algorithm);
            _isHash = IsHash(_algorithm);

			_se = null;
			_he = null;

			switch(_algorithm) 
			{
				case AlgorithmType.BlowFish :
					_se = new SymmetricAlgorithmEngine(SymmetricAlgorithmEngine.EncodeMethodEnum.BlowFish);
					break;
				case AlgorithmType.DES :
					_se = new SymmetricAlgorithmEngine(SymmetricAlgorithmEngine.EncodeMethodEnum.DES);
					break;
				case AlgorithmType.MD5 :
					_he = new HashAlgorithmEngine(HashAlgorithmEngine.EncodeMethodEnum.MD5);
					break;
				case AlgorithmType.RC2 :
					_se = new SymmetricAlgorithmEngine(SymmetricAlgorithmEngine.EncodeMethodEnum.RC2);
					break;
				case AlgorithmType.Rijndael :
					_se = new SymmetricAlgorithmEngine(SymmetricAlgorithmEngine.EncodeMethodEnum.Rijndael);
					break;
				case AlgorithmType.SHA :
					_he = new HashAlgorithmEngine(HashAlgorithmEngine.EncodeMethodEnum.SHA);
					break;
				case AlgorithmType.SHA256 :
					_he = new HashAlgorithmEngine(HashAlgorithmEngine.EncodeMethodEnum.SHA256);
					break;
				case AlgorithmType.SHA384 :
					_he = new HashAlgorithmEngine(HashAlgorithmEngine.EncodeMethodEnum.SHA384);
					break;
				case AlgorithmType.SHA512 :
					_he = new HashAlgorithmEngine(HashAlgorithmEngine.EncodeMethodEnum.SHA512);
					break;
				case AlgorithmType.TripleDES :
					_se = new SymmetricAlgorithmEngine(SymmetricAlgorithmEngine.EncodeMethodEnum.TripleDES);
					break;
				case AlgorithmType.Twofish :
					_se = new SymmetricAlgorithmEngine(SymmetricAlgorithmEngine.EncodeMethodEnum.Twofish);
					break;
                case AlgorithmType.Blake256:
                    _he = new HashAlgorithmEngine(HashAlgorithmEngine.EncodeMethodEnum.Blake256);
                    break;
                case AlgorithmType.Blake512:
                    _he = new HashAlgorithmEngine(HashAlgorithmEngine.EncodeMethodEnum.Blake512);
                    break;
				default :
					return false;
			}
			return true;
		}
		
		public class HashAlgorithmEngine
		{
			public enum EncodeMethodEnum : int
			{
				SHA, SHA256, SHA384, SHA512, MD5,Blake512, Blake256
			}

			private HashAlgorithm EncodeMethod;

			public HashAlgorithmEngine(HashAlgorithm ServiceProvider)
			{
				EncodeMethod = ServiceProvider;
			}

			public HashAlgorithmEngine(EncodeMethodEnum iSelected)
			{
				switch (iSelected)
				{
					case EncodeMethodEnum.SHA:
						EncodeMethod = new SHA1CryptoServiceProvider();
						break;
					case EncodeMethodEnum.SHA256:
						EncodeMethod = new SHA256Managed();
						break;
					case EncodeMethodEnum.SHA384:
						EncodeMethod = new SHA384Managed();
						break;
					case EncodeMethodEnum.SHA512:
						EncodeMethod = new SHA512Managed();
						break;
					case EncodeMethodEnum.MD5:
						EncodeMethod = new MD5CryptoServiceProvider();
						break;
                    case EncodeMethodEnum.Blake256:
                        EncodeMethod = new Blake256();
                        break;
                    case EncodeMethodEnum.Blake512:
                        EncodeMethod = new Blake512();
                        break;
				}
			}

			public string Encoding(string Source)
			{
				byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
				
                byte[] bytOut = EncodeMethod.ComputeHash(bytIn);
			
				// convert into Base64 so that the result can be used in xml
				return System.Convert.ToBase64String(bytOut, 0, bytOut.Length);
			}
		}
		
		public class SymmetricAlgorithmEngine
		{
			public enum EncodeMethodEnum : int
			{
				DES, RC2, Rijndael, TripleDES, BlowFish, Twofish
			}

			private SymmetricAlgorithm EncodeMethod;

			public SymmetricAlgorithmEngine(SymmetricAlgorithm ServiceProvider)
			{
				EncodeMethod = ServiceProvider;
			}

			public SymmetricAlgorithmEngine(EncodeMethodEnum iSelected)
			{
				switch (iSelected)
				{
					case EncodeMethodEnum.DES:
						EncodeMethod = new DESCryptoServiceProvider();
						break;
					case EncodeMethodEnum.RC2:
						EncodeMethod = new RC2CryptoServiceProvider();
						break;
					case EncodeMethodEnum.Rijndael:
						EncodeMethod = new RijndaelManaged();
						break;
					case EncodeMethodEnum.TripleDES:
						EncodeMethod = new TripleDESCryptoServiceProvider();
						break;
					case EncodeMethodEnum.BlowFish:
						EncodeMethod = new BlowfishAlgorithm();
						EncodeMethod.Mode = CipherMode.CBC;
						EncodeMethod.KeySize = 40;
						EncodeMethod.GenerateKey();
						EncodeMethod.GenerateIV();
						break;
					case EncodeMethodEnum.Twofish:
						EncodeMethod = new Twofish();
						EncodeMethod.Mode = CipherMode.CBC;
						EncodeMethod.GenerateKey();
						EncodeMethod.GenerateIV();
						break;

				}
			}

			private byte[] getValidKey(string Key)
			{
				string sTemp;
				if (EncodeMethod.LegalKeySizes.Length > 0)
				{
					int lessSize = 0, moreSize = EncodeMethod.LegalKeySizes[0].MinSize;
					// key sizes are in bits
				
					while (Key.Length * 8 > moreSize && 
						   EncodeMethod.LegalKeySizes[0].SkipSize > 0 && 
						   moreSize < EncodeMethod.LegalKeySizes[0].MaxSize)
					{
						lessSize = moreSize;
						moreSize += EncodeMethod.LegalKeySizes[0].SkipSize;
					}

					if(Key.Length * 8 > moreSize)
						sTemp = Key.Substring(0, (moreSize / 8));
					else
						sTemp = Key.PadRight(moreSize / 8, ' ');
				}
				else
					sTemp = Key;
				// convert the secret key to byte array
				return ASCIIEncoding.ASCII.GetBytes(sTemp);
			}

			private byte[] getValidIV(String InitVector, int ValidLength)
			{
				if( InitVector.Length > ValidLength ) 
				{
					return ASCIIEncoding.ASCII.GetBytes(InitVector.Substring(0, ValidLength));
				} 
				else 
				{
					return ASCIIEncoding.ASCII.GetBytes(InitVector.PadRight(ValidLength,' '));
				}
			}

			public static string BufToStr(byte[] buf)
			{
				StringBuilder result = new StringBuilder(buf.Length * 3);

				for (int nI = 0, nC = buf.Length; nI < nC; nI++)
				{
					if (0 < nI) result.Append(' ');
					result.Append(buf[nI].ToString("x2"));
				}
				return result.ToString();
			}

			public string Encrypting(string Source, string Key)
			{
				if(Source==null || Key==null || Source.Length==0 || Key.Length == 0)
					return null;
				
				if(EncodeMethod == null) return "Under Construction";

				long lLen;
				int nRead, nReadTotal;
				byte[] buf = new byte[3];
				byte[] srcData;
				byte[] encData;
				System.IO.MemoryStream sin;
				System.IO.MemoryStream sout;
				CryptoStream encStream;

				srcData = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
				sin = new MemoryStream();
				sin.Write(srcData,0,srcData.Length);
				sin.Position = 0;
				sout = new MemoryStream();
				
				EncodeMethod.Key = getValidKey(Key);
				EncodeMethod.IV = getValidIV(Key, EncodeMethod.IV.Length); 

				encStream = new CryptoStream(sout, 
					EncodeMethod.CreateEncryptor(), 
					CryptoStreamMode.Write);
				lLen = sin.Length;
				nReadTotal = 0;
				while (nReadTotal < lLen)
				{
					nRead = sin.Read(buf, 0, buf.Length);
					encStream.Write(buf, 0, nRead);
					nReadTotal += nRead;
				}
				encStream.Close();  
				
				encData = sout.ToArray();
				// create a MemoryStream so that the process can be done without I/O files
				//System.IO.MemoryStream ms = new System.IO.MemoryStream();

				//byte[] bytKey = getValidKey(Key);
				// set the private key
				//EncodeMethod.Key = getValidKey(Key);
				//EncodeMethod.IV = getValidIV(Key, EncodeMethod.IV.Length); 

				// create an Encryptor from the Provider Service instance
				//ICryptoTransform encrypto = EncodeMethod.CreateEncryptor();

				// create Crypto Stream that transforms a stream using the encryption
				//CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

				// write out encrypted content into MemoryStream
				//cs.Write(bytIn, 0, bytIn.Length);
				//cs.FlushFinalBlock();
				//cs.Close();
            
				// get the output and trim the '\0' bytes
				//byte[] bytOut = ms.GetBuffer();
				//int i = 0;
				//for (i = 0; i < bytOut.Length; i++)
				//	if (bytOut[i] == 0)
				//		break;
                
				//byte[] bytOut = ms.ToArray();
				// convert into Base64 so that the result can be used in xml
				return System.Convert.ToBase64String(encData);
				//return System.Convert.ToBase64String(bytOut, 0, i);
			}

			public string Decrypting(string Source, string Key)
			{
				if(Source==null || Key==null || Source.Length==0 || Key.Length == 0)
					return null;
				
				if(EncodeMethod == null) return "Under Construction";

				long lLen;
				int nRead, nReadTotal;
				byte[] buf = new byte[3];
				byte[] decData;
				byte[] encData;
				System.IO.MemoryStream sin;
				System.IO.MemoryStream sout;
				CryptoStream decStream;

				encData = System.Convert.FromBase64String(Source);
				sin = new MemoryStream(encData);
				sout = new MemoryStream();
				
				EncodeMethod.Key = getValidKey(Key);
				EncodeMethod.IV = getValidIV(Key, EncodeMethod.IV.Length); 

				decStream = new CryptoStream(sin, 
					EncodeMethod.CreateDecryptor(), 
					CryptoStreamMode.Read);
				
				lLen = sin.Length;
				nReadTotal = 0;
				while (nReadTotal < lLen)
				{
					nRead = decStream.Read(buf, 0, buf.Length);
					if (0 == nRead) break;
			
					sout.Write(buf, 0, nRead);
					nReadTotal += nRead;
				}
				//long n = sout.Length;
				
				decStream.Close();  

				decData = sout.ToArray();
				
				// convert from Base64 to binary
				//byte[] bytIn = System.Convert.FromBase64String(Source);
				// create a MemoryStream with the input
				//System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

				// set the private key
				//EncodeMethod.Key = getValidKey(Key);
				//EncodeMethod.IV = getValidIV(Key, EncodeMethod.IV.Length);

				// create a Decryptor from the Provider Service instance
				//ICryptoTransform encrypto = EncodeMethod.CreateDecryptor();
 
				// create Crypto Stream that transforms a stream using the decryption
				//CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

				// read out the result from the Crypto Stream
				//System.IO.StreamReader sr = new System.IO.StreamReader( decStream );
				//string result = sr.ReadToEnd();
				//decStream.Close();
				//sr.Close();
				
				
				ASCIIEncoding ascEnc = new ASCIIEncoding();
				return ascEnc.GetString(decData);
				
				//return n.ToString();
			}

		}
	}
}
