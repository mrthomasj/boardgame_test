using BankruptTest.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankruptTest.Config
{
    class BoardConfig
    {
        //TODO: Implementar dados de propriedade para iteração dos players

        
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"gameConfig.txt");
        public List<Propriedade> boardProperties = new List<Propriedade>();

        public void LoadBoard()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(@"Arquivo de configuração não encontrado, favor verificar se há um arquivo 'gameConfig.txt' na pasta raíz.");
                return;
            }
            try
            {
                string[] configLines = File.ReadAllLines(path);
                foreach (string s in configLines)
                {
                    Console.WriteLine(s);
                    string[] tempProperty = s.Split();
                    Propriedade dummyProperty = new Propriedade();

                    for (int i = 0; i < tempProperty.Length; i++)
                    {
                        if (dummyProperty.Cost == 0 && tempProperty[i] != "")
                        {
                            dummyProperty.Cost = int.Parse(tempProperty[i]);
                        }
                        else if (dummyProperty.Rent == 0 && tempProperty[i] != "")
                        {
                            dummyProperty.Rent = int.Parse(tempProperty[i]);
                        }
                        if (boardProperties.Count == 0)
                        {
                            dummyProperty.Id = 1;
                        }
                        else
                        {
                            dummyProperty.Id = boardProperties.Count + 1;
                        }
                    }
                    boardProperties.Add(dummyProperty);
                }

                Console.WriteLine("--------------");

                foreach (Propriedade property in boardProperties)
                {
                    Console.WriteLine($"O custo da propriedade {property.Id} é: {property.Cost}");
                    Console.WriteLine($"O custo do aluguel da propriedade {property.Id} é: {property.Rent}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Arquivo de configuração apresenta erros na leitura, favor verificar se ele atende ao padrao: <número inteiro> <número inteiro>.");
                Environment.Exit(010);
            }
           
        }

    }
}
