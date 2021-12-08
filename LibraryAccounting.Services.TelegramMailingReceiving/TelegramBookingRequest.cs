using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Services.TelegramMailingReceiving
{
    public class TelegramBookingRequest
    {
        public string Email { get; set; }
        public int BookId { get; set; }
        public long ChatId { get; set; }
    }
}
