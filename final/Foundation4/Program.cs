using System;

class Activity
{
    protected DateTime date;
    protected int length; // in minutes

    public Activity(DateTime date, int length)
    {
        this.date = date;
        this.length = length;
    }

    public virtual double GetDistance()
    {
        return 0;
    }

    public virtual double GetSpeed()
    {
        return 0;
    }

    public virtual double GetPace()
    {
        return 0;
    }

    public virtual string GetSummary()
    {
        return $"{date.ToShortDateString()} {GetType().Name} ({length} min)";
    }
}

class Running : Activity
{
    private double distance; // in miles

    public Running(DateTime date, int length, double distance)
        : base(date, length)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / length) * 60;
    }

    public override double GetPace()
    {
        return length / distance;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

class Cycling : Activity
{
    private double speed; // in mph

    public Cycling(DateTime date, int length, double speed)
        : base(date, length)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed * length) / 60;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return 60 / speed;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

class Swimming : Activity
{
    private int laps;
    private const double LapLength = 50; // in meters

    public Swimming(DateTime date, int length, int laps)
        : base(date, length)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return (laps * LapLength) / 1000 * 0.62; // Convert to miles
    }

    public override double GetSpeed()
    {
        return (GetDistance() / length) * 60;
    }

    public override double GetPace()
    {
        return length / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Activity[] activities = {
            new Running(new DateTime(2024, 5, 1), 30, 3),
            new Cycling(new DateTime(2024, 5, 2), 45, 20),
            new Swimming(new DateTime(2024, 5, 3), 60, 40)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
