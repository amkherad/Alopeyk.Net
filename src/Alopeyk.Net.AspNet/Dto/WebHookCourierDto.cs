namespace Alopeyk.Net.AspNet.Dto
{
    public class WebHookCourierDto    {
        public string phone { get; set; } 
        public string firstname { get; set; } 
        public string lastname { get; set; } 
        public WebHookAvatarDto avatar { get; set; } 
        public WebHookAbsAvatarDto abs_avatar { get; set; } 
        public WebHookLastPositionDto last_position { get; set; } 
    }
}