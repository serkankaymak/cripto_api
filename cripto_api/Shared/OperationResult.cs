using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;
public abstract class AResult
{
    public bool isSuccess { get; private set; }
    string Message { get; set; }

    protected AResult(string message, bool isSuccess = false)
    {
        Message = message;
        this.isSuccess = isSuccess;
    }
}
public class OperationResult : AResult
{
    public OperationResult(string message, bool isSuccess = false) : base(message, isSuccess)
    {
    }

    public static OperationResult Success()
    {
        return new OperationResult("Success", true);
    }
    public static OperationResult Success(string message)
    {
        return new OperationResult(message, true);
    }
    public static OperationResult Fail()
    {
        return new OperationResult("Success", false);
    }
    public static OperationResult Fail(string message)
    {
        return new OperationResult(message, false);
    }

}

