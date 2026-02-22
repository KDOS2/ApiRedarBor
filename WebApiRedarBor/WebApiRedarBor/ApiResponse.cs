namespace WebApiRedarBor
{
    public class ApiResponse<T>
    {

        public int status { get; set; } = 200;
        public bool success { get; set; } = true;
        public T? data { get; set; }        
    }
}
