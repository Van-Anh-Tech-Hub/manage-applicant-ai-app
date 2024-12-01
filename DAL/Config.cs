using System;

namespace DAL
{
    internal class Config
    {
        public static string MongoName
        {
            get
            {
                return GetAppSetting("MONGO_NAME", true);
            }
        }
        public static string MONGO_URL
        {
            get
            {
                return GetAppSetting("MONGO_URL", true);
            }
        }

        /// <summary>
        /// Lấy giá trị cấu hình từ App.config hoặc Properties.Settings.Default theo tên key.
        /// </summary>
        /// <param name="key">Tên của key trong AppSettings.</param>
        /// <param name="required">Có yêu cầu không. Nếu true và key không tồn tại, sẽ ném ra ngoại lệ.</param>
        /// <returns>Giá trị của key hoặc chuỗi rỗng nếu không tồn tại.</returns>
        /// <exception cref="InvalidOperationException">Ném ngoại lệ nếu key không tồn tại và required là true.</exception>
        public static string GetAppSetting(string key, bool required = false)
        {
            string value = Properties.Settings.Default[key] as string;

            if (string.IsNullOrEmpty(value) && required)
            {
                throw new InvalidOperationException($"'{key}' là bắt buộc nhưng không tồn tại hoặc không có giá trị.");
            }

            return value ?? string.Empty;
        }
    }
}
