using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace AppCompresion
{
    public class AppCompresion
    {

		public string ComprimirDataset(DataSet ds)
		{

			string Retorno;
			MemoryStream memStream;
			DeflateStream compressedStream;

			Retorno = string.Empty;
			memStream = new MemoryStream();
			compressedStream = new DeflateStream(memStream , CompressionMode.Compress , true);

			//Serializar en XML directamente al stream comprimido
			ds.WriteXml(compressedStream , XmlWriteMode.WriteSchema);
			compressedStream.Close();
			memStream.Seek(0 , System.IO.SeekOrigin.Begin);
			//Convierte a cadena con digitos base 64 del stream comprimido
			Retorno = Convert.ToBase64String(memStream.GetBuffer() , Base64FormattingOptions.None);

			return Retorno;

		}

		public DataSet DesomprimirDataset(string base64EncodedData)
		{

			DataSet Retorno;
			MemoryStream memStream;
			DeflateStream compressedStream;

			Retorno = new DataSet();

			if ((base64EncodedData.Length) > 0)
			{
				memStream = new MemoryStream(Convert.FromBase64String(base64EncodedData));
				compressedStream = new DeflateStream(memStream, CompressionMode.Decompress, true);
				//Convierte en XML comprimido en un dataset
				Retorno.ReadXml(compressedStream);
			}
			else
				Retorno = null;

			return Retorno;

		}

        public async Task<DataSet> DesomprimirDatasetAsync(string base64EncodedData)
        {

            DataSet Retorno;
            MemoryStream memStream;
            DeflateStream compressedStream;

            Retorno = new DataSet();

            if ((base64EncodedData.Length) > 0)
            {
                memStream = new MemoryStream(Convert.FromBase64String(base64EncodedData));
                compressedStream = new DeflateStream(memStream, CompressionMode.Decompress, true);
                //Convierte en XML comprimido en un dataset
                await Task.Run(() => { Retorno.ReadXml(compressedStream); }); 
            }
            else
                Retorno = null;

            return Retorno;

        }


    }

}
