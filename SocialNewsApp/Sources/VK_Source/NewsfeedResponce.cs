namespace SocialNewsApp.Sources.VK_Source
{
    public class NewsfeedResponce
    {
        public PostsResponse response { get; set; }
    }

    public class PostsResponse
    {
        public PostItem[] items { get; set; }
        public Profile[] profiles { get; set; }
        public Group[] groups { get; set; }
        public Reaction_Sets[] reaction_sets { get; set; }
        public string next_from { get; set; }
    }

    public class PostItem
    {
        public string type { get; set; }
        public int source_id { get; set; }
        public int date { get; set; }
        public string inner_type { get; set; }
        public int carousel_offset { get; set; }
        public float short_text_rate { get; set; }
        public Donut donut { get; set; }
        public Comments comments { get; set; }
        public int marked_as_ads { get; set; }
        public bool can_set_category { get; set; }
        public bool can_doubt_category { get; set; }
        public Attachment[] attachments { get; set; }
        public int edited { get; set; }
        public int id { get; set; }
        public bool is_favorite { get; set; }
        public Likes likes { get; set; }
        public string reaction_set_id { get; set; }
        public Reactions reactions { get; set; }
        public int owner_id { get; set; }
        public int post_id { get; set; }
        public Post_Source post_source { get; set; }
        public string post_type { get; set; }
        public Reposts reposts { get; set; }
        public string text { get; set; }
        public Views views { get; set; }
        public Sharing sharing { get; set; }
        public string donut_miniapp_url { get; set; }
        public bool zoom_text { get; set; }
        public bool marked_as_author_ad { get; set; }
        public Copyright copyright { get; set; }
        public Copy_History[] copy_history { get; set; }
        public int signer_id { get; set; }
    }

    public class Donut
    {
        public bool is_donut { get; set; }
    }

    public class Comments
    {
        public int can_post { get; set; }
        public int can_view { get; set; }
        public int count { get; set; }
        public bool groups_can_post { get; set; }
    }

    public class Likes
    {
        public int can_like { get; set; }
        public int count { get; set; }
        public int user_likes { get; set; }
        public int can_publish { get; set; }
        public bool repost_disabled { get; set; }
    }

    public class Reactions
    {
        public int count { get; set; }
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public int id { get; set; }
        public int count { get; set; }
    }

    public class Post_Source
    {
        public string platform { get; set; }
        public string type { get; set; }
    }

    public class Reposts
    {
        public int count { get; set; }
        public int user_reposted { get; set; }
    }

    public class Views
    {
        public int count { get; set; }
    }

    public class Sharing
    {
        public Target[] targets { get; set; }
    }

    public class Target
    {
        public string name { get; set; }
        public string track_code { get; set; }
    }

    public class Copyright
    {
        public string link { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int id { get; set; }
    }

    public class Attachment
    {
        public string type { get; set; }
        public Photo photo { get; set; }
        public Link link { get; set; }
        public Video video { get; set; }
    }

    public class Photo
    {
        public int album_id { get; set; }
        public int date { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string access_key { get; set; }
        public Size[] sizes { get; set; }
        public string text { get; set; }
        public int user_id { get; set; }
        public string web_view_token { get; set; }
        public bool has_tags { get; set; }
        public Tags tags { get; set; }
        public int post_id { get; set; }
    }

    public class Tags
    {
        public int count { get; set; }
    }

    public class Size
    {
        public int height { get; set; }
        public string type { get; set; }
        public int width { get; set; }
        public string url { get; set; }
    }

    public class Link
    {
        public string url { get; set; }
        public string description { get; set; }
        public bool is_favorite { get; set; }
        public string title { get; set; }
        public string target { get; set; }
        public string caption { get; set; }
        public Photo1 photo { get; set; }
    }

    public class Photo1
    {
        public int album_id { get; set; }
        public int date { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public Size1[] sizes { get; set; }
        public string text { get; set; }
        public int user_id { get; set; }
        public string web_view_token { get; set; }
        public bool has_tags { get; set; }
    }

    public class Size1
    {
        public int height { get; set; }
        public string type { get; set; }
        public int width { get; set; }
        public string url { get; set; }
    }

    public class Video
    {
        public string response_type { get; set; }
        public string access_key { get; set; }
        public int can_comment { get; set; }
        public int can_like { get; set; }
        public int can_repost { get; set; }
        public int can_subscribe { get; set; }
        public int can_add_to_faves { get; set; }
        public int can_add { get; set; }
        public int comments { get; set; }
        public int date { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public Image[] image { get; set; }
        public First_Frame[] first_frame { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public bool is_favorite { get; set; }
        public string track_code { get; set; }
        public string type { get; set; }
        public int views { get; set; }
        public int local_views { get; set; }
        public int can_dislike { get; set; }
        public int repeat { get; set; }
        public string ov_id { get; set; }
        public string live_status { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int with_padding { get; set; }
    }

    public class First_Frame
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Copy_History
    {
        public string inner_type { get; set; }
        public string type { get; set; }
        public int date { get; set; }
        public int from_id { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public Post_Source1 post_source { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
    }

    public class Post_Source1
    {
        public string platform { get; set; }
        public string type { get; set; }
    }

    public class Profile
    {
        public int id { get; set; }
        public int sex { get; set; }
        public string screen_name { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public Online_Info online_info { get; set; }
        public int online { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool can_access_closed { get; set; }
        public bool is_closed { get; set; }
        public int online_mobile { get; set; }
        public int online_app { get; set; }
    }

    public class Online_Info
    {
        public bool visible { get; set; }
        public bool is_online { get; set; }
        public bool is_mobile { get; set; }
        public int last_seen { get; set; }
        public int app_id { get; set; }
    }

    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public int is_closed { get; set; }
        public string type { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public string photo_200 { get; set; }
    }

    public class Reaction_Sets
    {
        public string id { get; set; }
        public Item2[] items { get; set; }
    }

    public class Item2
    {
        public int id { get; set; }
        public string title { get; set; }
        public Asset asset { get; set; }
    }

    public class Asset
    {
        public string animation_url { get; set; }
        public Image1[] images { get; set; }
        public Title title { get; set; }
        public Title_Color title_color { get; set; }
    }

    public class Title
    {
        public Color color { get; set; }
    }

    public class Color
    {
        public Foreground foreground { get; set; }
        public Background background { get; set; }
    }

    public class Foreground
    {
        public string light { get; set; }
        public string dark { get; set; }
    }

    public class Background
    {
        public string light { get; set; }
        public string dark { get; set; }
    }

    public class Title_Color
    {
        public string light { get; set; }
        public string dark { get; set; }
    }

    public class Image1
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

}
