
namespace Apps.MVCApp.Models
{
    public class JsonResultModel
    {
        /// <summary>
        /// Флаг успеха операции
        /// </summary>
        public bool IsOk { get; set; }
        /// <summary>
        /// Сообщение об ошибке, которое можно показать пользователю
        /// </summary>
        public string ErrMessage { get; set; }
        /// <summary>
        /// Данные, если они есть
        /// </summary>
        public object Data { get; set; }

        public string URL { get; set; }
        public JsonResultModel()
        {

        }
        public JsonResultModel(bool isOk = false, string errMessage = null, object data = null, string url = null)
        {
            IsOk = isOk;
            ErrMessage = errMessage;
            Data = data;
            URL = url;
        }
    }
}
