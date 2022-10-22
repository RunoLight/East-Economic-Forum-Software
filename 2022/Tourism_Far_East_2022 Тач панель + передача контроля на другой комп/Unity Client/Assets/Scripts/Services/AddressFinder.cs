// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Net.NetworkInformation;
// using System.Threading;
// using System.Threading.Tasks;
// using UnityEngine;
// using Ping = System.Net.NetworkInformation.Ping;
//
// public class AddressFinder {
//
//     public List<string> addresses = new List<string>();
//     private MonoBehaviour m;
//
//     Thread myThread = null;
//     public int instances = 0;
//
//     public AddressFinder(MonoBehaviour mono) {
//         m = mono;
//     }
//
//     public void Scan(List<IPSegment> iPSegments, Action<List<string>> addresses) {
//         myThread = new Thread(() => ScanThreads(iPSegments, addresses));
//         myThread.Start();
//     }
//
//     private void ScanThreads(List<IPSegment> iPSegments, Action<List<string>> callback) {
//         Ping myPing;
//         PingReply reply;
//         instances = 0;
//
//         foreach(IPSegment ips in iPSegments) {
//             foreach(uint hosta in ips.Hosts()) {
//                 string ip = IpHelper.ToIpString(hosta);
//                 Task.Factory.StartNew(() => {
//                     myPing = new Ping();
//                     instances++;
//                     reply = myPing.Send(ip, 250);
//
//                     if(reply.Status == IPStatus.Success) {
//                         addresses.Add(ip);
//                     }
//                     instances--;
//                 }, TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning);
//             }
//         }
//         int i = 0;
//         while(instances > 0) {
//             if(i > 100)
//                 break;
//             i++;
//             Thread.Sleep(10);
//         }
//         callback(addresses);
//     }
// }
//
// public class IPSegment {
//
//     private UInt32 _ip;
//     private UInt32 _mask;
//
//     public IPSegment(string ip, string mask) {
//         _ip = ip.ParseIp();
//         _mask = mask.ParseIp();
//     }
//
//     public UInt32 NumberOfHosts {
//         get { return ~_mask+1; }
//     }
//
//     public UInt32 NetworkAddress {
//         get { return _ip & _mask; }
//     }
//
//     public UInt32 BroadcastAddress {
//         get { return NetworkAddress + ~_mask; }
//     }
//
//     public IEnumerable<UInt32> Hosts(){
//         for (var host = NetworkAddress+1; host < BroadcastAddress; host++) {
//             yield return  host;
//         }
//     }
//
// }
//
// public static class IpHelper {
//     public static string ToIpString(this UInt32 value) {
//         var bitmask = 0xff000000;
//         var parts = new string[4];
//         for (var i = 0; i < 4; i++) {
//             var masked = (value & bitmask) >> ((3-i)*8);
//             bitmask >>= 8;
//             parts[i] = masked.ToString(CultureInfo.InvariantCulture);
//         }
//         return String.Join(".", parts);
//     }
//
//     public static UInt32 ParseIp(this string ipAddress) {
//         var splitted = ipAddress.Split('.');
//         UInt32 ip = 0;
//         for (var i = 0; i < 4; i++) {
//             ip = (ip << 8) + UInt32.Parse(splitted[i]);
//         }
//         return ip;
//     }
// }