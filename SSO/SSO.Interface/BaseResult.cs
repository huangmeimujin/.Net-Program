using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SSO.Interface
{
    public class BaseResult
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        public static DataResult<T> CreateDataResult<T>(T data,bool? isSuccess,string msg="")
        {
            return new DataResult<T>() {
                IsSuccess = isSuccess??data!=null,
                Msg=msg,
                Result=data
            };
        }

        public static ListResult<T> CreateListResult<T>(IEnumerable<T> datas,bool? isSuccess,string msg="",int? total=null)
        {
            return new ListResult<T>() {
                IsSuccess = isSuccess ?? datas != null,
                Msg = msg,
                Result = datas,
                Total=total??datas?.Count()??0
            };
        }


    }

    public abstract class BaseResult<T> : BaseResult
    {
        public T Result { get; set; }
    }

    public class DataResult<T> : BaseResult<T>
    {

    }

    public class ListResult<T> : BaseResult<IEnumerable<T>>
    {
        public long Total { get; set; }
    }
}
