using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using ResortTools.SelfReg.Interfaces;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.ViewModels;
using UnityAPI.ClientLibrary.Interfaces;
using UnityModels = UnityAPI.Models;
using UnityAPI.Models.Requests;

namespace ResortTools.SelfReg.Services
{
    public class WaiverServiceMock : IWaiverService
    {
        //private readonly IContactProvider _contactProvider;

        public WaiverServiceMock(IContactProvider contactProvider)
        {
            //_contactProvider = contactProvider;
        }

        public SearchResult<WaiverViewModel> GetByAuthCode(string AuthCode, int TerminalId)
        {
            //TODO:  Don't know if Terminal ID is actually needed here, but passing it just in case
            SearchResult<WaiverViewModel> result = new SearchResult<WaiverViewModel>();

            List<WaiverViewModel> waiverVms = new List<WaiverViewModel>();
            WaiverViewModel wvm = new WaiverViewModel
            {
                WaiverText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Eget duis at tellus at urna condimentum mattis pellentesque. At tellus at urna condimentum mattis pellentesque. Laoreet id donec ultrices tincidunt. Non curabitur gravida arcu ac tortor dignissim. Elementum curabitur vitae nunc sed velit dignissim sodales ut eu. Amet est placerat in egestas erat imperdiet sed euismod. Faucibus pulvinar elementum integer enim neque volutpat ac tincidunt vitae. Scelerisque mauris pellentesque pulvinar pellentesque habitant. Malesuada fames ac turpis egestas maecenas pharetra convallis posuere. " +
                    "Condimentum vitae sapien pellentesque habitant morbi tristique senectus et netus.Tincidunt vitae semper quis lectus nulla at volutpat.Nulla at volutpat diam ut venenatis tellus in metus vulputate.Pellentesque pulvinar pellentesque habitant morbi.Odio eu feugiat pretium nibh.Tortor vitae purus faucibus ornare suspendisse sed.Aliquam ultrices sagittis orci a scelerisque.In ornare quam viverra orci sagittis eu.Lectus sit amet est placerat in egestas erat imperdiet.Tincidunt ornare massa eget egestas purus.Ornare arcu dui vivamus arcu felis bibendum ut tristique et.Sit amet dictum sit amet justo donec enim diam.Tellus id interdum velit laoreet id donec ultrices"  +
                    "tincidunt.Et netus et malesuada fames ac turpis egestas maecenas pharetra.Sit amet porttitor eget dolor morbi non.Arcu non odio euismod lacinia at quis risus.Lorem ipsum dolor sit amet.Vel turpis nunc eget lorem dolor sed viverra ipsum.Molestie a iaculis at erat pellentesque adipiscing commodo elit at.Quis imperdiet massa tincidunt nunc pulvinar sapien et ligula. " + 
                    "Dolor sit amet consectetur adipiscing elit.Euismod nisi porta lorem mollis.Viverra nibh cras pulvinar mattis nunc sed blandit.Commodo elit at imperdiet dui accumsan sit amet nulla facilisi.Malesuada proin libero nunc consequat.Eget mi proin sed libero enim sed faucibus turpis in.Vestibulum morbi blandit cursus risus at.Amet mauris commodo quis imperdiet massa tincidunt.Amet dictum sit amet justo donec enim diam vulputate ut.Quis enim lobortis scelerisque fermentum dui.Vulputate sapien nec sagittis aliquam.Pellentesque diam volutpat commodo sed egestas egestas." + 
                    "Sed adipiscing diam donec adipiscing tristique risus.Semper eget duis at tellus at urna.Laoreet suspendisse interdum consectetur libero.Egestas pretium aenean pharetra magna.Ipsum dolor sit amet consectetur adipiscing elit ut aliquam purus.Rutrum tellus pellentesque eu tincidunt tortor.Massa tempor nec feugiat nisl pretium fusce.Consequat interdum varius sit amet mattis vulputate enim nulla.Sagittis orci a scelerisque purus semper.Id leo in vitae turpis massa sed elementum tempus egestas.Fringilla ut morbi tincidunt augue interdum velit.Ut lectus arcu bibendum at varius"
            };
            waiverVms.Add(wvm);

            result.Results = waiverVms;

            return result;
        }

        // * AddWaiver
        // * This function adds data from a waiver 
        public UpdateResult<Waiver> AddWaiver(Waiver Waiver, int TerminalClientCode)
        {
            //TODO:  Don't know if Terminal ID is actually needed here, but passing it just in case
            //TODO: Fake data
            Waiver NewWaiver = new Waiver();
            NewWaiver.Minors = Waiver.Minors;
            NewWaiver.Signer = Waiver.Signer;
            NewWaiver.WaiverText = Waiver.WaiverText;
            NewWaiver.SignatureString = Waiver.SignatureString;
            NewWaiver.SignatureBase64String = Waiver.SignatureBase64String;


            UpdateResult<Waiver> result = new UpdateResult<Waiver>
            {
                Status = "OK",
                UpdatedRecord = NewWaiver
            };

            return result;
        }

    }
}
