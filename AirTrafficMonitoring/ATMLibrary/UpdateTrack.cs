using System;
using System.Collections.Generic;
using System.Linq;
using ATMLibrary.Interfaces;

namespace ATMLibrary
{
   public class UpdateTrack : IUpdateTrack
    {
        public List<ITrackData> OldList { get; set; }
        private ITrackData _renderTrack;   
        private IDetectVicinity _vicinity;
        

        public double TimeSpan { get; set; }
        public UpdateTrack(ITrackData renderTrack, IDetectVicinity vicinity)
        {
            _renderTrack = renderTrack;
            _vicinity = vicinity;
            OldList = new List<ITrackData>();
        }
     

        public void Update(List<ITrackData> newList)
        {
            foreach (var newTrack in newList)
            {
                if (!OldList.Any())
                    break;

                foreach (var oldTrack in OldList)
                {
                    if (newTrack.Tag == oldTrack.Tag)
                    {
                        newTrack.Speed = CalSpeed(newTrack, oldTrack);
                        newTrack.Course = CalCourse(newTrack, oldTrack);
                    }
                }
            }

            OldList.Clear();

            foreach (var trackData in newList)
            {
                OldList.Add(trackData);
            }

            _renderTrack.Print(newList);
            _vicinity.CheckVicinity(newList);
        }

        public int CalSpeed(ITrackData track1, ITrackData track2)
        {
            TimeSpan time = track2.Timestamp - track1.Timestamp;
            TimeSpan = (double)time.TotalSeconds;

            double dX = 0;
            double dY = 0;
            double speed = 0;

            if (track1.X > track2.X)
            {
                dX = track1.X - track2.X;
            }
            else
            {
                dX = track2.X - track1.X;
            }

            if (track1.Y > track2.Y)
            {
                dY = track1.Y - track2.Y;
            }
            else
            {
                dY = track2.Y - track1.Y;
            }

            double distance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

            if (TimeSpan < 0)
            {
                TimeSpan = TimeSpan * -1;
                speed = distance / TimeSpan;
            }
            else if (TimeSpan > 0)
            { speed = distance / TimeSpan; }

            return (int)speed;
        }

        public int CalCourse(ITrackData track1, ITrackData track2)
        {
            double dX = track2.X - track1.X;
            double dY = track2.Y - track1.Y;
            double Degree = 0;

            if (dX == 0)
            {
                // if deltaY er større end 0
                // condition ? first_expression : second_expression;
                Degree = dY > 0 ? 0 : 180;
            }
            else
            {
                double radian = Math.Atan2(dY, dX);
                //Convert to degress 
                Degree = radian / Math.PI * 180;

                Degree = 90 - Degree;
                if (Degree < 0)
                {
                    Degree += 360;
                }
            }

            return (int)Degree;
        }

    }
}
