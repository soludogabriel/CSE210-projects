using System;
using System.Collections.Generic;

class Comment
{
    private string name;
    private string text;

    public Comment(string name, string text)
    {
        this.name = name;
        this.text = text;
    }

    public string GetCommentDetails()
    {
        return $"{name}: {text}";
    }
}

class Video
{
    private string title;
    private string author;
    private int length;
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        this.title = title;
        this.author = author;
        this.length = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Author: {author}");
        Console.WriteLine($"Length: {length} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        foreach (var comment in comments)
        {
            Console.WriteLine(comment.GetCommentDetails());
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Video 1", "Author 1", 300);
        video1.AddComment(new Comment("User A", "Great video!"));
        video1.AddComment(new Comment("User B", "Thanks for the info."));
        video1.AddComment(new Comment("User C", "Very helpful."));
        videos.Add(video1);

        Video video2 = new Video("Video 2", "Author 2", 600);
        video2.AddComment(new Comment("User D", "Amazing content!"));
        video2.AddComment(new Comment("User E", "I learned a lot."));
        videos.Add(video2);

        Video video3 = new Video("Video 3", "Author 3", 450);
        video3.AddComment(new Comment("User F", "Interesting perspective."));
        video3.AddComment(new Comment("User G", "Well explained."));
        video3.AddComment(new Comment("User H", "Thanks for sharing!"));
        videos.Add(video3);

        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}
