using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class OperationResult<TResultCode, TEntity> : OperationResponse<TResultCode>
        where TResultCode : struct, IConvertible
    {
        public TEntity Entity { get; }

        public OperationResult(TEntity entity = default(TEntity), TResultCode enumResult = default(TResultCode), ResponseMessageDto responseMessage = null)
            : base(enumResult, responseMessage)
        {
            Entity = entity;
        }

    }
    public class OperationResponse<TEnumResult>
        where TEnumResult : struct, IConvertible
    {
      
        public TEnumResult ResponseCode { get; }
        public ResponseMessageDto ResponseMessage { get; }

        public OperationResponse(TEnumResult enumResult = default(TEnumResult), ResponseMessageDto exception = null, ICollection<ValidationResult> validationResults = null)
        {
            ResponseCode = enumResult;
            ResponseMessage = exception;
        }
    } }

