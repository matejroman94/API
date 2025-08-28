using VZPStat.EventAsByte;

namespace VZPStatAPI.FakeData
{
    public class Faker
    {
        public IEnumerable<EventAsByte> GetFakeEvents_01h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "01" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_81h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "81" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_41h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 255);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "41" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_C1h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 255);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "C1" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_21h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = 0;
                p5 = 0;

                date = GetFakeDate();

                code = "21" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_A1h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = 0;
                p5 = 0;

                date = GetFakeDate();

                code = "A1" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_02h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "02" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_03h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = 255;
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "03" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_04h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            var extendedCode = new[] { "04", "84", "44", "24" };
            var res = new Bogus.Faker();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();


                var str = res.PickRandom(extendedCode);

                code = str + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_05h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            var extendedCode = new[] { "05", "85", "45", "25" };
            var res = new Bogus.Faker();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 255);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();


                var str = res.PickRandom(extendedCode);

                code = str + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_06h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            var extendedCode = new[] { "06", "86", "46", "26" };
            var res = new Bogus.Faker();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();


                var str = res.PickRandom(extendedCode);

                code = str + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_08h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            var extendedCode = new[] { "08", "88", "48", "28" };
            var res = new Bogus.Faker();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 255);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();


                var str = res.PickRandom(extendedCode);

                code = str + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_07h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            byte ran = 0;

            for (int i = 0; i < 1000; i++)
            {
                ran = random.Byte(0, 3);
                if (ran == 0)
                {
                    p0 = 255;
                    p1 = random.Byte(0, 255);
                    p2 = random.Byte(0, 255);
                    p3 = 255;
                    p4 = 255;
                    p5 = 255;
                }
                else if (ran == 1)
                {
                    p0 = random.Byte(0, 255);
                    p1 = 255;
                    p2 = random.Byte(0, 255);
                    p3 = random.Byte(2, 4);
                    if (random.Byte(250, 255) == 255) p3 = 255;
                    p4 = 255;
                    p5 = 255;
                }
                else if (ran == 2)
                {
                    p0 = 255;
                    p1 = 254;
                    p2 = random.Byte(0, 255);
                    p3 = 255;
                    p4 = 255;
                    p5 = 255;
                }
                else
                {
                    p0 = 254;
                    p1 = 255;
                    p2 = random.Byte(0, 255);
                    p3 = random.Byte(2, 4);
                    if (random.Byte(250, 255) == 255) p3 = 255;
                    p4 = 255;
                    p5 = 255;
                }

                date = GetFakeDate();

                code = "07" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_09h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 8);
                p2 = random.Byte(1, 8);
                p3 = 255;
                p4 = 255;
                p5 = 255;

                date = GetFakeDate();

                code = "09" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_0Ah()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "0A" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_0Bh()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 249);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "0B" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_10h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = 0;
                p1 = 0;
                p2 = 0;
                p3 = 255;
                p4 = 255;
                p5 = 255;

                date = GetFakeDate();

                code = "10" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_11h()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = 0;
                p1 = 0;
                p2 = 0;
                p3 = 255;
                p4 = 255;
                p5 = 255;

                date = GetFakeDate();

                code = "11" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_0Ch()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            var extendedCode = new[] { "02", "03", "06", "07", "08",
                "09", "0B","0E","0F", "14", "15" };

            var fakke = new Bogus.Faker();
            string str = "";

            for (int i = 0; i < 1000; i++)
            {
                str = fakke.PickRandom(extendedCode);

                p0 = 0;
                p1 = 0;
                p2 = 0;
                p3 = Convert.ToByte(str, 16);
                p4 = 255;
                p5 = 255;

                if (str.Equals("07") || str.Equals("08"))
                {
                    p4 = random.Byte(0, 255);
                }
                else if (str.Equals("09"))
                {
                    p4 = random.Byte(0, 255);
                    p5 = random.Byte(0, 255);
                }
                else if (str.Equals("0F"))
                {
                    p4 = random.Byte(1, 255);
                }
                else if (str.Equals("14"))
                {
                    p4 = random.Byte(1, 4);
                    p5 = random.Byte(0, 255);
                }
                else if (str.Equals("15"))
                {
                    p4 = random.Byte(0, 255);
                    p5 = random.Byte(0, 255);
                }

                date = GetFakeDate();

                code = "0C" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_0Dh()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = GetFakeDate();

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(1, 6);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 255);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "0D" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public IEnumerable<EventAsByte> GetFakeEvents_0Eh()
        {
            List<EventAsByte> events = new List<EventAsByte>();

            var random = new Bogus.Randomizer();
            byte p0;
            byte p1;
            byte p2;
            byte p3;
            byte p4;
            byte p5;
            string code;
            string date = string.Empty;

            for (int i = 0; i < 1000; i++)
            {
                p0 = random.Byte(0, 255);
                p1 = random.Byte(0, 255);
                p2 = random.Byte(0, 255);
                p3 = random.Byte(0, 255);
                p4 = random.Byte(0, 255);
                p5 = random.Byte(0, 255);

                date = GetFakeDate();

                code = "0E" + p0.ToString("X2")
                    + p1.ToString("X2")
                    + p2.ToString("X2")
                    + p3.ToString("X2")
                    + p4.ToString("X2")
                    + p5.ToString("X2");

                events.Add(new EventAsByte(code, date));
            }

            return events;
        }

        public string GetFakeDate()
        {
            return DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
