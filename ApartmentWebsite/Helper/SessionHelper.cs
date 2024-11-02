namespace ApartmentWebsite.Helper
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value, TimeSpan expiry)
        {
            // Tạo đối tượng chứa dữ liệu và thời gian hết
            var sessionData = new SessionData
            {
                Value = value,
                ExpiryTime = DateTime.UtcNow.Add(expiry)
            };

            // Chuyển đổi thành JSON để lưu vào session
            session.SetString(key, JsonConvert.SerializeObject(sessionData));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null) return default;

            var sessionData = JsonConvert.DeserializeObject<SessionData>(value);

            // Kiểm tra xem dữ liệu hết hạn
            if (sessionData.ExpiryTime < DateTime.UtcNow)
            {
                session.Remove(key); // Xóa 
                return default;
            }

            return (T)sessionData.Value;
        }

        // Lớp lưu giá trị và thời gian hết
        private class SessionData
        {
            public object Value { get; set; }
            public DateTime ExpiryTime { get; set; }
        }
    }
} 

