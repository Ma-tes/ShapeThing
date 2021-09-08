namespace MouseThing.Interfaces
{
    interface ICoordinates<T>
    {
        void Write();
        bool Compare(T objectC);
    }
}
