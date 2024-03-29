﻿using SEP3_TIER3.DTO;
using SEP3_TIER3.Model;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
namespace SEP3_TIER3.Server
{
    class ClientHandler
    {
        private TcpClient client;
        private ServerModel serverModel;
        public ClientHandler(TcpClient client, ServerModel serverModel)
        {
            this.client = client;
            this.serverModel = serverModel;
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }
        private Request ReceiveRequest(NetworkStream stream)
        {
            byte[] receiveLengthBytes = new byte[4];
            stream.Read(receiveLengthBytes);
            int receiveLength = BitConverter.ToInt32(receiveLengthBytes, 0);
            byte[] receiveBytes = new byte[receiveLength];
            stream.Read(receiveBytes);
            String rcv = Encoding.ASCII.GetString(receiveBytes);
            return JsonSerializer.Deserialize<Request>(rcv);
        }
        private void SendPlanes(NetworkStream stream, List<Plane> planes)
        {
            Request request = new Request { Type = "RESPONSEPLANES", Planes = planes };
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            int length = Encoding.ASCII.GetByteCount(json);
            byte[] toSendBytes = Encoding.ASCII.GetBytes(json);
            byte[] toSendLengthBytes = BitConverter.GetBytes(length);
            stream.Write(toSendLengthBytes);
            stream.Write(toSendBytes);
        }
        private void SendNodes(NetworkStream stream, List<NodeDTO> nodesToSend)
        {
            Request request = new Request { Type = "RESPONSENODES", Nodes = nodesToSend };
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            int length = Encoding.ASCII.GetByteCount(json);
            byte[] toSendBytes = Encoding.ASCII.GetBytes(json);
            byte[] toSendLengthBytes = BitConverter.GetBytes(length);
            stream.Write(toSendLengthBytes);
            stream.Write(toSendBytes);
        }
        private void SendEdges(NetworkStream stream, List<EdgeDTO> edgesToSend)
        {
            Request request = new Request { Type = "RESPONSEEDGES", Edges = edgesToSend };
            var json = JsonSerializer.Serialize(request);
            int length = Encoding.ASCII.GetByteCount(json);
            byte[] toSendBytes = Encoding.ASCII.GetBytes(json);
            byte[] toSendLengthBytes = BitConverter.GetBytes(length);
            stream.Write(toSendLengthBytes);
            stream.Write(toSendBytes);
        }

        /*private void SendNodesWithEdges(NetworkStream stream, List<GroundNodeDTO> nodesToSend)
        {
            var json = JsonSerializer.Serialize(nodesToSend);
            int length = Encoding.ASCII.GetByteCount(json);
            byte[] toSendBytes = Encoding.ASCII.GetBytes(json);
            byte[] toSendLengthBytes = BitConverter.GetBytes(length);
            stream.Write(toSendLengthBytes);
            stream.Write(toSendBytes);
        }*/
        private void Run()
        {
            NetworkStream stream = client.GetStream();
            try
            {
                while (true)
                {
                    Request request = ReceiveRequest(stream);
                    switch (request.Type)
                    {
                        case "REQUESTPLANES":
                            {
                                if (serverModel.Planes.Count == 0)
                                {
                                    serverModel.LoadPlanes();
                                }
                                SendPlanes(stream, serverModel.Planes);
                                break;
                            }
                        case "REQUESTNODES":
                            {
                                if (serverModel.Nodes.Count == 0)
                                {
                                    serverModel.LoadNodes();
                                }
                                SendNodes(stream, serverModel.NodesDTO);
                                break;
                            }
                        case "REQUESTEDGES":
                            {
                                if (serverModel.Edges.Count == 0)
                                {
                                    serverModel.LoadEdges();
                                }
                                SendEdges(stream, serverModel.EdgesDTO);
                                break;
                            }
                        case "DELETEFLIGHTPLAN":
                            {
                                serverModel.DeleteFlightPlan(request.flightPlanToDelete);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                stream.Close();
                client.Close();
            }
        }
        public class Request
        {
            public string Type { get; set; }
            public List<Plane> Planes { get; set; }
            public List<EdgeDTO> Edges { get; set; }
            public List<NodeDTO> Nodes { get; set; }
            public string flightPlanToDelete { get; set; }
        }
    }
}