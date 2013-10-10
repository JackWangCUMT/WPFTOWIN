//ManiX Tuple
using System;

namespace ManiX
{
    [Serializable]
    public class Tuple<T1>
    {
        private T1 m_Item1;

        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }

        public Tuple(T1 item1)
        {
            m_Item1 = item1;
        }
    }

    [Serializable]
    public class Tuple<T1, T2>
    {
        private T1 m_Item1;
        private T2 m_Item2;

        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }

        public T2 Item2
        {
            get { return m_Item2; }
            set { m_Item2 = value; }
        }

        public Tuple(T1 item1, T2 item2)
        {
            m_Item1 = item1;
            m_Item2 = item2;
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3>
    {
        private T1 m_Item1;
        private T2 m_Item2;
        private T3 m_Item3;


        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }
        public T2 Item2
        {
            get { return m_Item2; }
            set { m_Item2 = value; }
        }
        public T3 Item3
        {
            get { return m_Item3; }
            set { m_Item3 = value; }
        }

        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4>
    {
        private T1 m_Item1;
        private T2 m_Item2;
        private T3 m_Item3;
        private T4 m_Item4;

        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }
        public T2 Item2
        {
            get { return m_Item2; }
            set { m_Item2 = value; }
        }
        public T3 Item3
        {
            get { return m_Item3; }
            set { m_Item3 = value; }
        }
        public T4 Item4
        {
            get { return m_Item4; }
            set { m_Item4 = value; }
        }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5>
    {
        private T1 m_Item1;
        private T2 m_Item2;
        private T3 m_Item3;
        private T4 m_Item4;
        private T5 m_Item5;

        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }
        public T2 Item2
        {
            get { return m_Item2; }
            set { m_Item2 = value; }
        }
        public T3 Item3
        {
            get { return m_Item3; }
            set { m_Item3 = value; }
        }
        public T4 Item4
        {
            get { return m_Item4; }
            set { m_Item4 = value; }
        }
        public T5 Item5
        {
            get { return m_Item5; }
            set { m_Item5 = value; }
        }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5, T6>
    {
        private T1 m_Item1;
        private T2 m_Item2;
        private T3 m_Item3;
        private T4 m_Item4;
        private T5 m_Item5;
        private T6 m_Item6;

        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }
        public T2 Item2
        {
            get { return m_Item2; }
            set { m_Item2 = value; }
        }
        public T3 Item3
        {
            get { return m_Item3; }
            set { m_Item3 = value; }
        }
        public T4 Item4
        {
            get { return m_Item4; }
            set { m_Item4 = value; }
        }
        public T5 Item5
        {
            get { return m_Item5; }
            set { m_Item5 = value; }
        }
        public T6 Item6
        {
            get { return m_Item6; }
            set { m_Item6 = value; }
        }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
            m_Item6 = item6;
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5, T6, T7>
    {

        private T1 m_Item1;
        private T2 m_Item2;
        private T3 m_Item3;
        private T4 m_Item4;
        private T5 m_Item5;
        private T6 m_Item6;
        private T7 m_Item7;

        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }
        public T2 Item2
        {
            get { return m_Item2; }
            set { m_Item2 = value; }
        }
        public T3 Item3
        {
            get { return m_Item3; }
            set { m_Item3 = value; }
        }
        public T4 Item4
        {
            get { return m_Item4; }
            set { m_Item4 = value; }
        }
        public T5 Item5
        {
            get { return m_Item5; }
            set { m_Item5 = value; }
        }
        public T6 Item6
        {
            get { return m_Item6; }
            set { m_Item6 = value; }
        }
        public T7 Item7
        {
            get { return m_Item7; }
            set { m_Item7 = value; }
        }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
            m_Item6 = item6;
            m_Item7 = item7;
        }
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>
    {

        private T1 m_Item1;
        private T2 m_Item2;
        private T3 m_Item3;
        private T4 m_Item4;
        private T5 m_Item5;
        private T6 m_Item6;
        private T7 m_Item7;
        private TRest m_Rest;

        public T1 Item1
        {
            get { return m_Item1; }
            set { m_Item1 = value; }
        }
        public T2 Item2
        {
            get { return m_Item2; }
            set { m_Item2 = value; }
        }
        public T3 Item3
        {
            get { return m_Item3; }
            set { m_Item3 = value; }
        }
        public T4 Item4
        {
            get { return m_Item4; }
            set { m_Item4 = value; }
        }
        public T5 Item5
        {
            get { return m_Item5; }
            set { m_Item5 = value; }
        }
        public T6 Item6
        {
            get { return m_Item6; }
            set { m_Item6 = value; }
        }
        public T7 Item7
        {
            get { return m_Item7; }
            set { m_Item7 = value; }
        }
        public TRest Rest
        {
            get { return m_Rest; }
            set { m_Rest = value; }
        }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
            m_Item6 = item6;
            m_Item7 = item7;
            m_Rest = rest;
        }
    }
}