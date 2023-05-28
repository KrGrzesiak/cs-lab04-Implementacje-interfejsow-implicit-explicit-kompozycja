using System;

namespace ver4
{
    public interface IDevice
    {
        enum State {on, off, standby};

        void PowerOn() => SetState(State.on);           // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff() => SetState(State.off);         // wyłącza urządzenie, zmienia stan na `off
        void StandbyOn() => SetState(State.standby);    // Uruchamia oszczędzanie energii.
        void StandbyOff() => SetState(State.on);        // Wyłącza oszczędzanie energii.

        abstract protected void SetState(State state);

        State GetState(); // zwraca aktualny stan urządzenia

        int Counter {get;}  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                            // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }

}
