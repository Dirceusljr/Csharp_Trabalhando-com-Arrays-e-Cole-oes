namespace bytebank_ATENDIMENTO.byteBank.Exception;


[Serializable]
public class ByteBankException : System.Exception
{
	public ByteBankException() { }
	public ByteBankException(string message) : base("Aconteceu uma exceção ->" + message) { }
	public ByteBankException(string message, System.Exception inner) : base(message, inner) { }
	protected ByteBankException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
