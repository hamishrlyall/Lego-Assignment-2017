﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lego.Ev3.Desktop;
using Lego.Ev3.Core;
using System.Threading;

namespace LegoClasses
{
    public class Legobot
    {
        public Brick brick { get; set; }

        public async void ConnectToBrick()
        {
            brick = new Brick(new BluetoothCommunication("COM4"));
            await brick.ConnectAsync();
            brick.Ports[InputPort.Four].SetMode(ColorMode.Color);
        }

        public async void Forward()
        {
            await brick.DirectCommand.TurnMotorAtSpeedForTimeAsync(OutputPort.A | OutputPort.D, 20, 250, false);
        }

        public async void Reverse()
        {
            await brick.DirectCommand.TurnMotorAtSpeedForTimeAsync(OutputPort.A | OutputPort.D, -35, 500, false);
        }

        public async void Stop()
        {
            await brick.DirectCommand.TurnMotorAtSpeedForTimeAsync(OutputPort.A | OutputPort.D, 0, 2000, false);
        }

        public async void Turn15Right()
        {
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, 300, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -40, 300, false);
            await brick.BatchCommand.SendCommandAsync();
        }

        public async void Turn15Left()
        {
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -40, 300, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 40, 300, false);
            await brick.BatchCommand.SendCommandAsync();
        }

        public async void Turn90Right()
        {
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, 1000, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -40, 1000, false);
            await brick.BatchCommand.SendCommandAsync();
        }

        public async void Turn90Left()
        {
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, 1000, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -40, 1000, false);
            await brick.BatchCommand.SendCommandAsync();
        }

        public async void BaseFound()
        {
            await brick.DirectCommand.PlayToneAsync(100, 1000, 1000);
        }

        public float DetectDistance()
        {
            float vDistance = brick.Ports[InputPort.Two].SIValue;
            if (vDistance > 143)
            {
                Reverse();
            }
            return brick.Ports[InputPort.Two].SIValue;
        }

        public float DetectColour()
        {
            float[] Colours = new float[10];
            int i = 0;
            while (i < 10)
            {
                Colours[i] = Colours[i] + brick.Ports[InputPort.Four].SIValue;
                Console.WriteLine("ColourArray" + Colours[i]);
                i++;
                Thread.Sleep(100);

            }

            Colours.Sum();
            Colours.Average();
            float ColourDetected = Colours.Average();
            return ColourDetected;
        }
    }
}
