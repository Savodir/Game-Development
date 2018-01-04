﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Vancluysen.Carl
{
    public class Animation
    {
        private List<AnimationFrame> frames;
        public AnimationFrame CurrentFrame { get; set; }
        public int FramesPerS { get; set; }
        private int counter;

        public int Counter
        {
            get { return counter; }
            set { counter = value; }
        }


        private double x = 0;
        public double offset { get; set; }

        private int totalWidth = 0;

        public Animation()
        {
            frames = new List<AnimationFrame>();
            FramesPerS = 1;
        }
        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                //Duration = duration
            };

            frames.Add(newFrame);
            CurrentFrame = frames[0];
            offset = CurrentFrame.SourceRectangle.Width;
            foreach (AnimationFrame f in frames)
                totalWidth += f.SourceRectangle.Width;
        }


        public void Update(GameTime gameTime)
        {
            double temp = CurrentFrame.SourceRectangle.Width * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);

            x += temp;
            if (x >= CurrentFrame.SourceRectangle.Width / FramesPerS)
            {
                Console.WriteLine(x);
                x = 0;
                counter++;
                if (counter >= frames.Count)
                    counter = 0;
                CurrentFrame = frames[counter];
                offset += CurrentFrame.SourceRectangle.Width;
            }
            if (offset >= totalWidth)
                offset = 0;
        }
    }
}

