namespace TravelAgency.Shared.Travel
{
    public class SeatNumbers
    {
        public static List<string> AssignRandomSeatNumbers(int passengerCount)
        {
            List<string> seatNumbers = new();

            for (int i = 0; i < passengerCount; i++)
            {
                string seatNumber = GenerateRandomSeatNumber(seatNumbers);
                seatNumbers.Add(seatNumber);
            }

            return seatNumbers;
        }

        private static string GenerateRandomSeatNumber(List<string> assignedSeats)
        {
            string alphabet = "ABCDEFGHJKL";
            string numbers = "123456";
            string seatNumber;

            do
            {
                char row = alphabet[Random.Shared.Next(alphabet.Length)];
                string seat = numbers[Random.Shared.Next(numbers.Length)].ToString();
                seatNumber = $"{row}{seat}";
            } while (assignedSeats.Contains(seatNumber) || assignedSeats.Contains(GetAdjacentSeat(seatNumber)));

            return seatNumber;
        }

        private static string GetAdjacentSeat(string seatNumber)
        {
            char row = seatNumber[0];
            int seat = int.Parse(seatNumber.Substring(1));

            int adjacentSeat = seat + 1;
            string adjacentSeatNumber = $"{row}{adjacentSeat}";

            return adjacentSeatNumber;
        }
    }
}
