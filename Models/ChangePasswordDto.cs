using System.Text.Json.Serialization;

public class ChangePasswordDto
{
    public long Id { get; set; }

    [JsonPropertyName("currentPassword")]
    public string CurrentPassword { get; set; }

    [JsonPropertyName("newPassword")]
    public string NewPassword { get; set; }
}