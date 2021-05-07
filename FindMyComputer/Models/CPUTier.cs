namespace FindMyComputer.Models
{
    /// <summary>
    /// e.g Tier 1: FX, Core. Tier 2: Athlon, Celeron. For newer CPU, ryzen, it could be tier 1 too.
    /// </summary>
    public class CPUTier
    {
        public string Keyword { get; set; }
        public int Tier { get; set; }
    }
}