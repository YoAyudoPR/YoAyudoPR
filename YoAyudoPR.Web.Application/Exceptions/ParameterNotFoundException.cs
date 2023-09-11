using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class ParameterNotFoundException : Exception
	{
        public ParameterNotFoundException(string? paramType, string? paramId)
        : base($"Parameter not found | Param Type: {paramType} | Param Id: {paramId}")
        {
            ParamType = paramType;
            ParamId = paramId;
        }

        public ParameterNotFoundException(string? paramType, string? paramId, Exception innerException)
            : base($"Parameter not found | Param Type: {paramType} | Param Id: {paramId}", innerException)
        {
            ParamType = paramType;
            ParamId = paramId;
        }

        public string? ParamType { get; set; }
        public string? ParamId { get; set; }
    }
}

