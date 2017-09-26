﻿using FlooringMastery.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.IO;

namespace FlooringMastery.Data
{
    public class LiveDataRepository : IOrderRepository
    {
        string _filepath = null;
        public LiveDataRepository(string filepath)
        {
            _filepath = filepath;
        }
       
        List<Order> IOrderRepository.LoadOrders(string OrderDate)
        {
                List<Order> Orders = new List<Order>();
                string filename = "Orders_" + OrderDate + ".txt";
                var fileToRead = _filepath + filename;
                if (File.Exists(fileToRead))
                {
                    var reader = File.ReadAllLines(fileToRead);
                    for (int i = 1; i < reader.Length; i++)
                    {
                        var columns = reader[i].Split(',');
                        var order = new Order();

                        order.OrderNumber = columns[0];
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.Tax = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSquareFoot = decimal.Parse(columns[6]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        order.MaterialCost = decimal.Parse(columns[8]);
                        order.LaborCost = decimal.Parse(columns[9]);
                        order.Tax = decimal.Parse(columns[10]);
                        order.Total = decimal.Parse(columns[11]);

                        Orders.Add(order);
                        
                    }
                }
                else
                {

                    Console.ReadKey();
                }
                return Orders;
            }
        }
    }

