using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryRobotIII
{
    class Program
    {
        static void Main(string[] args)
        {
            String readLine = "";
            readLine = Console.ReadLine();
            //Console.WriteLine(" Input Found  : " + readLine);
            //Console.ReadLine();

            //-----------------Counting Charecters : 

            Char[] inputClone = readLine.ToCharArray();
            int countAlpha = 0;
            int countQuoma = 0;

            for (int j = 0; j < inputClone.Length; j++)
            {
                //Console.WriteLine("inputClone[] : " + inputClone[j]);
                // if (String.Equals( inputClone[j], (String)"N") )
                if (Char.Equals(inputClone[j], 'N'))
                    countAlpha++;
                if (Char.Equals(inputClone[j], 'E'))
                    countAlpha++;
                if (Char.Equals(inputClone[j], 'W'))
                    countAlpha++;
                if (Char.Equals(inputClone[j], 'S'))
                    countAlpha++;
                if (Char.Equals(inputClone[j], ','))
                    countQuoma++;
            }




            if (countQuoma == 0) {

                if (countAlpha == 1) {
                    //Console.WriteLine(" countAlpha : "+ countAlpha);
                    //Console.WriteLine(" countQuoma : " + countQuoma);
                    //Console.ReadLine();
                    for (int i = 1; i < inputClone.Length; i++) {
                        if (!Char.IsDigit(inputClone[i])) {
                            //  break;
                        }
                        String trim = readLine.Substring(1, readLine.Length - 1);
                        Console.WriteLine(trim);
                        Console.ReadLine();
                    }
                }
            }
            //------------------------------Another Case for countQuoma : 
            if (countQuoma > 0) {
                if (countAlpha > 1)
                {

                    LinkedList<String> directionList = new LinkedList<String>();
                    LinkedList<int> distList = new LinkedList<int>();

                    var bits = readLine.Split(',');
                    int[] distance = new int[bits.Length];

                    distance = getDistance(bits);

                    directionList = getDirection(bits);

                    int totalDistance = getTotalDistance(distance);

                    for (int ls = 0; ls < distance.Length; ls++)
                    {
                        distList.AddLast(distance[ls]);

                    }

                    // ... matrice of (x , y) ::
                    int[,] matrcBlock = new int[totalDistance * 2, totalDistance * 2];

                    for (int r = 0; r < (totalDistance * 2); r++)
                    {
                        //String plotToScreen="";
                        for (int s = 0; s < (totalDistance * 2); s++)
                        {
                            matrcBlock[r, s] = 0;
                            // plotToScreen += " " + 0;

                        }
                        //Console.WriteLine(plotToScreen+'\n');
                    }

                    int[] midPoint = new int[2];
                    midPoint[0] = totalDistance;
                    midPoint[1] = totalDistance;


                    matrcBlock = plotPath(0, midPoint, distList, matrcBlock, directionList);

                    int count = 0;

                    for (int t = 0; t < totalDistance * 2; t++)
                    {
                        for (int q = 0; q < totalDistance * 2; q++)
                        {
                            if (matrcBlock[t, q] == 1)
                            {
                                count++;
                            }
                        }
                    }

                    Console.WriteLine(count);
                    int rightTurns = 0;
                    LinkedList<String> newDirList = new LinkedList<string>();
                    newDirList = getDirection(bits);
                    //newDirList = getDirection(0,directionList);
                    rightTurns = getRightTurns(0, newDirList);
                    if (rightTurns > 0) {
                        Console.WriteLine("");
                        Console.WriteLine("Right Turns : " + rightTurns);
                    }
                    Console.ReadLine();
                }
            }

        }
        //----------------------------------Close Of main Method <<-- :


        //----------------------------------Start Of Computative Methods
        public static int[,] plotPath(int count, int[] midPoint, LinkedList<int> distLst, int[,] matrx, LinkedList<String> lst)
        {

            //int followCoordinate1 = 0;
            //Console.WriteLine(" Bool true / false : " + String.Equals(lst.First().ToString(), ("N").ToString()));

            if (lst.Count() > 0)
            {
                // Start Plotting one direction at a time ..
                if (count == 0)
                {
                    //Console.WriteLine(" count ==0 if statement 0");
                    int followCoordinate = 0;
                    //int followCoordinate1 = 0;

                    if (String.Equals((String)lst.First(), (String)"N"))
                    { // N-> +Y 

                        for (int one = 1; one <= distLst.First(); one++)
                        {
                            matrx[midPoint[0], midPoint[1] + one] = 1;
                            followCoordinate = one;
                        }
                        midPoint[0] = midPoint[0];
                        midPoint[1] = midPoint[1] + followCoordinate;
                        count++;
                        // direction = reducedDirection(direction);
                        // distance = reducedDistance(distance);
                        distLst.RemoveFirst();
                        lst.RemoveFirst();
                        if (lst.Count() >= 1)
                        {
                            plotPath(count, midPoint, distLst, matrx, lst);
                        }
                        if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes
                    }
                    if (String.Equals(lst.First(), (String)"S"))
                    { // S -> -Y
                        for (int two = 1; two <= distLst.First(); two++)
                        {
                            matrx[midPoint[0], midPoint[1] - two] = 1;
                            followCoordinate = two;
                        }
                        midPoint[0] = midPoint[0];
                        midPoint[1] = midPoint[1] - followCoordinate;
                        count++;
                        //direction = reducedDirection(direction);
                        //distance = reducedDistance(distance);
                        if (lst.Count() >= 1)
                        {
                            distLst.RemoveFirst();
                            lst.RemoveFirst();

                            plotPath(count, midPoint, distLst, matrx, lst);
                        }
                        if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes

                    }
                    if (String.Equals((String)lst.First(), (String)"E"))
                    { // E -> +X

                        for (int three = 1; three <= distLst.First(); three++)
                        {
                            matrx[midPoint[0] + three, midPoint[0]] = 1;
                            followCoordinate = three;
                        }
                        midPoint[0] = midPoint[0] + followCoordinate;
                        midPoint[1] = midPoint[1];
                        count++;
                        //direction = reducedDirection(direction);
                        //distance = reducedDistance(distance);

                        if (lst.Count() >= 1)
                        {
                            distLst.RemoveFirst();
                            lst.RemoveFirst();
                            plotPath(count, midPoint, distLst, matrx, lst);
                        }
                        if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes
                    }
                    if (String.Equals((String)lst.First(), (String)"W"))
                    { // W -> -X
                        for (int four = 1; four <= distLst.First(); four++)
                        {
                            matrx[midPoint[0] - four, midPoint[1]] = 1;
                            followCoordinate = four;
                        }
                        midPoint[0] = midPoint[0] - followCoordinate;
                        midPoint[1] = midPoint[1];
                        count++;
                        //direction = reducedDirection(direction);
                        //distance = reducedDistance(distance);
                        distLst.RemoveFirst();
                        lst.RemoveFirst();
                        if (lst.Count() >= 1)
                        {
                            plotPath(count, midPoint, distLst, matrx, lst);
                        }
                        else if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////


                if (count > 0 && lst.Count() > 0)
                {

                    //int followCoordinate = 0;
                    int followCoordinate1 = 0;
                    int iteration = 1;

                    if (String.Equals((String)lst.First(), (String)"N"))
                    { // N-> +Y 
                        for (int one = 1; one <= distLst.First(); one++)
                        {
                            matrx[midPoint[0], midPoint[1] + one] = 1;
                            followCoordinate1 = one;
                        }
                        midPoint[0] = midPoint[0];
                        midPoint[1] = midPoint[1] + followCoordinate1;
                        iteration++;
                        distLst.RemoveFirst();
                        lst.RemoveFirst();
                        if (lst.Count() >= 1)
                        {
                            plotPath(iteration, midPoint, distLst, matrx, lst);
                        }
                        if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes
                    }
                    if (String.Equals((String)lst.First(), (String)"S"))
                    { // S -> -Y
                        for (int two = 1; two <= distLst.First(); two++)
                        {
                            matrx[midPoint[0], midPoint[1] - two] = 1;
                            followCoordinate1 = two;
                        }
                        midPoint[0] = midPoint[0];
                        midPoint[1] = midPoint[1] - followCoordinate1;
                        iteration++;
                        distLst.RemoveFirst();
                        lst.RemoveFirst();
                        if (lst.Count() >= 1)
                        {
                            plotPath(iteration, midPoint, distLst, matrx, lst);
                        }
                        if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes

                    }
                    if (String.Equals((String)lst.First(), (String)"E"))
                    { // E -> +X
                        //Console.WriteLine(" count > 0 if statement 3");
                        //Console.WriteLine(" plotPath( Count : " + count);
                        //Console.WriteLine(" count > 0 322 direction : " + lst.First());
                        //Console.WriteLine(" plotPath( direction array size : " + lst.Count());
                        //Console.WriteLine(" Actual distance : " + distLst.First());

                        for (int three = 1; three <= distLst.First(); three++)
                        {
                            matrx[midPoint[0] + three, midPoint[1]] = 1;
                            followCoordinate1 = three;
                        }
                        midPoint[0] = midPoint[0] + followCoordinate1;
                        midPoint[1] = midPoint[1];
                        iteration++;
                        //direction = reducedDirection(direction);
                        //distance = reducedDistance(distance);
                        distLst.RemoveFirst();
                        lst.RemoveFirst();
                        if (lst.Count() >= 1)
                        //Console.WriteLine(" Line 329 : direction lst size : " + lst.Count());
                        {
                            plotPath(iteration, midPoint, distLst, matrx, lst);
                            // plotPath(iteration, midPoint, , reducedDistance(distance), matrx);
                        }
                        if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes
                    }
                    if (String.Equals(lst.First(), (String)"W"))
                    { // W -> -X

                        for (int four = 1; four <= distLst.First(); four++)
                        {
                            matrx[(midPoint[0] - four), midPoint[1]] = 1;
                            followCoordinate1 = four;
                        }
                        midPoint[0] = midPoint[0] - followCoordinate1;
                        midPoint[1] = midPoint[1];
                        iteration++;
                        //direction = reducedDirection(direction);
                        //distance = reducedDistance(distance);
                        distLst.RemoveFirst();
                        lst.RemoveFirst();
                        if (lst.Count() >= 1)
                        {
                            plotPath(iteration, midPoint, distLst, matrx, lst);
                        }
                        if (lst.Count() == 0)
                        {
                            return matrx;
                        }
                        // take away last known direction, return function if #sizeOf Direction > 0 ..
                        // middlePoint changes
                        // Size of distance also changes
                    }


                    // take away last known direction, return function if #sizeOf Direction > 0 ..
                    // middlePoint changes
                    // Size of distance also changes
                    //return plotPath(midPoint, direction, distance, matrx);
                } // count > 0



            } // close if distance==0  
            if (lst.Count() == 0)
            {
                return matrx;
            }

            return matrx;
        }




        public static int getTotalDistance(int[] distance)
        {
            int Sum = 0;
            foreach (var c in distance)
            {
                Sum += c;
            }
            return Sum;
        }


        public static int[] getDistance(String[] inputBits)
        {
            int[] distance = new int[inputBits.Length];

            for (int i = 0; i < inputBits.Length; i++)
            {
                String dist = inputBits[i];

                int trim = int.Parse(dist.Substring(1, inputBits[i].Length - 1));  // input  { N2,W5,S6 } 

                distance[i] = trim;
                //Console.WriteLine(" Line 91:  trim  : " + trim);
            }

            return distance;

        }


        public static LinkedList<String> getDirection(String[] inputBits)
        {
            LinkedList<String> direction = new LinkedList<String>();
            for (int i = 0; i < inputBits.Length; i++)
            {
                String direc = inputBits[i];
                String trim = direc.Substring(0, 1);
                direction.AddLast(trim);
            }
            return direction;

        }




        public static int getRightTurns(int turnCount, LinkedList<String> dirLst) {
            int rightTurns = 0;

            if (dirLst.Count() > 1) {
                String firstDirection = dirLst.First();
                String secondDirection = dirLst.ElementAt(1);

                if (String.Equals((String)firstDirection, (String)"N")) {
                    if (String.Equals((String)secondDirection, (String)"E")) { turnCount++; }
                }
                if (String.Equals((String)firstDirection, (String)"E")) {
                    if (String.Equals((String)secondDirection, (String)"S")) { turnCount++; }
                }
                if (String.Equals((String)firstDirection, (String)"S")) {
                    if (String.Equals((String)secondDirection, (String)"W")) { turnCount++; }
                }
                if (String.Equals((String)firstDirection, (String)"W")) {
                    if (String.Equals((String)secondDirection, (String)"N")) { turnCount++; }
                }

                if (dirLst.Count() > 1) {
                    dirLst.RemoveFirst();
                    return getRightTurns(turnCount, dirLst);
                }

            }
            if (dirLst.Count()==1) {
                return turnCount;
            }

            return rightTurns;
        }

    }
}
