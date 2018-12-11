using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace QuickSort
{
    class Program
    {
        public class name
        {
            public name(string fname, string lname)
            {
                this.firstName = fname;
                this.lastName = lname;
            }
            public string firstName { get; set; }
            public string lastName { get; set; }
        }
        static void Main(string[] args)
        {
            List<name> Names = new List<name>();
            using (StreamReader sr = new StreamReader("names.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string[] s = sr.ReadLine().Split(' ');
                    Names.Add(new name(s[0], s[1]));

                }

            }
            EnumerateLists(Names);
            Console.WriteLine("Sorting.....");
            Stopwatch stopwatch = Stopwatch.StartNew();

            stopwatch.Stop();
        }
        /// <summary>
        ///     Swaps the two values of the specified indexes
        /// </summary>
        /// <param name="arr">An IList where elements need to be swapped</param>
        /// <param name="i">The first index to be swapped</param>
        /// <param name="j">The second index to be swapped</param>
        /// 

        private static void Swap<Names>(IList<Names> arr, int i, int j)
        {
            var tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }

        /// <summary>
        ///     Partitions an IList by defining a pivot and then comparing the other members to this pivot.
        /// </summary>
        /// <param name="arr">The IList to partition</param>
        /// <param name="low">The lowest index of the partition</param>
        /// <param name="high">The highest index of the partition</param>
        /// <returns>Returns the index of the chosen pivot</returns>
        private static int Partition<Names>(IList<Names> arr, int low, int high)
            where Names : IComparable<Names>
        {
            /*
                * Defining the pivot position, here the middle element is used but the choice of a pivot
                * is a rather complicated issue. Choosing the left element brings us to a worst-case performance,
                * which is quite a common case, that's why it's not used here.
                */
            var pivotPos = (high + low) / 2;
            var pivot = arr[pivotPos];
            // Putting the pivot on the left of the partition (lowest index) to simplify the loop
            Swap(arr, low, pivotPos);

            /** The pivot remains on the lowest index until the end of the loop
                * The left variable is here to keep track of the number of values
                * that were compared as "less than" the pivot.
                */
            var left = low;
            for (var i = low + 1; i <= high; i++)
            {
                // If the value is greater than the pivot value we continue to the next index.
                if (arr[i].CompareTo(pivot) >= 0) continue;

                // If the value is less than the pivot, we increment our left counter (one more element below the pivot)
                left++;
                // and finally we swap our element on our left index.
                Swap(arr, i, left);
            }

            // The pivot is still on the lowest index, we need to put it back after all values found to be "less than" the pivot.
            Swap(arr, low, left);

            // We return the new index of our pivot
            return left;
        }

        /// <summary>
        ///     Realizes a Quicksort on an IList of IComparable items in a sequential way.
        /// </summary>
        /// <param name="arr">The IList of IComaparable to Quicksort</param>
        /// <param name="left">The minimum index of the IList to Quicksort</param>
        /// <param name="right">The maximum index of the IList to Quicksort</param>
        private static void Quicksort<Names>(IList<Names> arr, int left, int right) where Names : IComparable<Names>
        {
            // If the list contains one or less element: no need to sort!
            if (right <= left) return;

            // Partitioning our list
            var pivot = Partition(arr, left, right);

            // Sorting the left of the pivot
            Quicksort(arr, left, pivot - 1);
            // Sorting the right of the pivot
            Quicksort(arr, pivot + 1, right);
        }

        /// <summary>
        ///     Realizes a Quicksort on an IList of IComparable items.
        ///     Left and right side of the pivot are processed in parallel.
        /// </summary>
        /// <param name="arr">The IList of IComaparable to Quicksort</param>
        /// <param name="left">The minimum index of the IList to Quicksort</param>
        /// <param name="right">The maximum index of the IList to Quicksort</param>
        private static void QuicksortParallel<Names>(IList<Names> arr, int left, int right)
            where Names : IComparable<Names>
        {
            // Defining a minimum length to use parallelism, over which using parallelism
            // got better performance than the sequential version.
            const int threshold = 2048;

            // If the list to sort contains one or less element, the list is already sorted.
            if (right <= left) return;

            // If the size of the list is under the threshold, sequential version is used.
            if (right - left < threshold)
                Quicksort(arr, left, right);

            else
            {
                // Partitioning our list and getting a pivot.
                var pivot = Partition(arr, left, right);

                // Sorting the left and right of the pivot in parallel
                Parallel.Invoke(
                    () => QuicksortParallel(arr, left, pivot - 1),
                    () => QuicksortParallel(arr, pivot + 1, right));
            }
        }
        public static void EnumerateLists(IList list)
        {
            foreach (object values in list)
            {
                Console.WriteLine(" ::", values.ToString());
            }
        }
    }
}