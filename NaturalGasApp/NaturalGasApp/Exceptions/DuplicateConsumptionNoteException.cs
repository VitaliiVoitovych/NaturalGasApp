namespace NaturalGasApp.Exceptions;

public class DuplicateConsumptionNoteException : Exception
{
    public DuplicateConsumptionNoteException() { }
    public DuplicateConsumptionNoteException(string message) : base(message) { }
    public DuplicateConsumptionNoteException(string message, Exception innerException)
        : base(message, innerException) { }

    public static void ThrowIfDuplicateExists(IEnumerable<NaturalGasConsumption> consumptions, NaturalGasConsumption consumption)
    {
        if (consumptions.Any(c => EqualsYearAndMonth(c.Date, consumption.Date)))
            throw new DuplicateConsumptionNoteException("A note for this month already exists");

        static bool EqualsYearAndMonth(DateOnly date1, DateOnly date2)
            => (date1.Month, date1.Year) == (date2.Month, date2.Year);
    }
}
