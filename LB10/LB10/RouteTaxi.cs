namespace LB10;

public class RouteTaxi
{
    public string RouteNumber;
    public decimal Fare;
    public string[] Stops;
    public int IntervalBetweenStopsMin;

    public int TotalTravelTime => Stops.Length * IntervalBetweenStopsMin;

    public override string ToString()
    {
        return $"Маршрут №{RouteNumber}, Ціна: {Fare} грн, Час: {TotalTravelTime} хв, Зупинки: {string.Join(" -> ", Stops)}";
    }  
}