namespace Stock.WebApi.DtoModels.User
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.UserValidation;

    public class LoginUserDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
