namespace API.Results;

public class ResultadoBase
{
    public bool Ok { get; set; } = true;
    public int StatusCode { get; set; }
    public string Error { get; set; }
    public void SetError(string error)
    {
        Ok = false;
        Error = error;
    }
}
