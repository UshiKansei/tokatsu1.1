using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokatsApplication_v1._1
{
    public class LogIn
    {
        [Required(ErrorMessage = "UserIDは必須です")]
        [StringLength(6, MinimumLength = 3, ErrorMessage = "UserIDは6文字です")]
        public required string UserID { get; set; }

        [Required(ErrorMessage = "パスワードは必須です")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "パスワードは英数小文字で6文字以上12文字以内で入力してください")]
        public required string UserPW { get; set; }

    }
}
