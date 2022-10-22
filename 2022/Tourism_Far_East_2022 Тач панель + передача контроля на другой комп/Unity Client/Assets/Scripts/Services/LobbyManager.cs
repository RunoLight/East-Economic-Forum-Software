// using System.Collections.Generic;
// using System.Net.NetworkInformation;
// using System.Net.Sockets;
// using UnityEngine;
// using Ping = UnityEngine.Ping;
//
// public class LobbyManager : MonoBehaviour {
//
//     public UnityEngine.Object[] Games;
//     private List<string> localAddresses = new List<string>();
//     private List<string> hostAddresses = new List<string>();
//     private Ping p;
//     AddressFinder af;
//
//     public void Start() {
//         List<IPSegment> iPSegments = GetInterfaces(true);
//         af = new AddressFinder(this);
//         af.Scan(
//             iPSegments,
//             (addresses) => {
//                 localAddresses = addresses;
//                 foreach(string address in localAddresses) {
//                     Debug.Log(address.ToString());
//                 }
//             }
//         );
//     }
//
//     public List<IPSegment> GetInterfaces(bool showVPN) {
//         List<IPSegment> ipsList = new List<IPSegment>();
//         foreach(NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces()) {
//             if(ni.Name.Contains("VM") || ni.Name.Contains("Loopback"))
//                 continue;
//             if(!showVPN && ni.Name.Contains("VPN"))
//                 continue;
//             foreach(UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses) {
//                 if(ip.Address.AddressFamily == AddressFamily.InterNetwork) {
//                     IPSegment ips = new IPSegment(ip.Address.ToString(), ip.IPv4Mask.ToString());
//                     ipsList.Add(ips);
//                 }
//             }
//         }
//         return ipsList;
//     }
// }