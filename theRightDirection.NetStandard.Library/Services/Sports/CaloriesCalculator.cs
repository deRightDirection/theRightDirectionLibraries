namespace theRightDirection.Services.Sports
{
    /// <summary>
    /// calculate the energy consumption for a sports activity
    /// </summary>
    public class CaloriesCalculator
    {
        /// <summary>
        /// Calculates the energy use by using the MET-method
        /// https://epi.grants.cancer.gov/atus-met/met.php
        /// MET hardlopen = 7,5 / binnen op de loopband 7,5
        /// MET zwemmen = 9,8
        /// MET wielrennen = 15,8 / fietsen op de fietstrainer 10 / mountainbiken 12,5
        /// MET basketbal = 8
        /// MET fitness = 3
        /// MET wandelen = 2,5
        /// </summary>
        /// <param name="duration">The duration in seconds</param>
        /// <param name="weight">The weight.</param>
        /// <param name="METValue">The met value.</param>
        /// <returns></returns>
        public int CalculateEnergyUse(int duration, double weight, double METValue)
        {
            var metValue = METValue;
            // metValue * 3,5
            var totalEnergy = metValue * 3.5;
            // vervolgens * gewicht
            totalEnergy = totalEnergy * weight;
            // vervolgens / 200
            totalEnergy = totalEnergy / 200;
            return (int)(totalEnergy * (duration / 60));
        }
    }
}