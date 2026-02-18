namespace Lab24
{
    public class ThresholdNotifierObserver
    {
        private double _threshold;

        public ThresholdNotifierObserver(double threshold)
        {
            _threshold = threshold;
        }

        public void OnResultCalculated(double result, string operationName)
        {
            if (result > _threshold)
                Console.WriteLine($"[⚠️ ПОПЕРЕДЖЕННЯ] Результат {result:F4} перевищує порогове значення {_threshold:F4} для операції '{operationName}'!");
        }

        public void SetThreshold(double threshold)
        {
            _threshold = threshold;
        }
    }
}
