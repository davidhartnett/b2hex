// David Hartnett
// 2015-10-26
// Parameters: flag input output

using System;
using System.IO;
using System.Text;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class b2hex
{
	public static void Main(string[] args)
	{
		if (args.Length < 3 || !File.Exists(args[1]))
		{
			Console.WriteLine("b2hex -(bh/hb) input output");
			return;
		}
		
		byte[] file_in = File.ReadAllBytes(args[1]);
		FileStream file_out = new FileStream(args[2], FileMode.Create);
		
		if (args[0].Equals("-hb"))
		{
			SoapHexBinary shb = SoapHexBinary.Parse(Encoding.ASCII.GetString(file_in, 0, file_in.Length));
			file_out.Write(shb.Value, 0, shb.Value.Length);
		}
		else
		{
			SoapHexBinary shb = new SoapHexBinary(file_in);
			
			byte[] utf16Bytes = Encoding.Unicode.GetBytes(shb.ToString());
			byte[] bytes = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, utf16Bytes);
			file_out.Write(bytes, 0, bytes.Length);
		}
		
		file_out.Close();
		return;
	}
}