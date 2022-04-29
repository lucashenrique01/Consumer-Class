# Consumidor C#

# Introdução


Example of a C# class that represents a generic consumer of a kakfa cluster.

## Tech

It only takes two libs for this class in C# to work.

- **Confluent.Kafka**> Allows you to read the topic in kafka.



- **Serilog.Sinks.Console** >  Allows you to perform console functions.








```
<ItemGroup>
<PackageReference Include="Confluent.Kafka" Version="1.4.0" />
<PackageReference Include="Serilog.Sinks.Console" Version="3.1.1" />
</ItemGroup>
```







## Instalação

To run this application you just need to open your terminal, go to the project directory and enter this command: "dotnet run "broker address", "topic_name" ."




## Recursos


This class has the function of listening to a specific topic and returning all new messages that arrive at the broker.
After to receive a message it is persisted in MySQL database and we have a function to check if this message exists in database. Case it exists it is only ignored, case not it is persisted. Thus ensuring the idempotence of the event

And to do that you enter the **broker address** and the **topic name** through environment variables.

**BROKER_HOST** > para o endereço do Broker.

**TOPIC_NAME** > para o nome do tópico. 







## Development







This project was initiative to my collegue Gabriel Araujo Dantas a brazilian Computer Engineer and me  Lucas Henrique A. de Paula a brazilian Computer Scientist.







## Docker







We begin the creation of Devops part that isn't the way we want, but we start the develop of this feature. For now, we have some important considerations:







- When you work with two docker-compose you need to create the network first (



  ```
  docker network create --driver=bridge  --subnet=172.18.0.0/16  --ip-range=172.18.0.0/24  --gateway=172.18.0.1   my_network
  ```



  ), this is necessary because you have use same network on all docker-compose



  



- After that you have to add on your kafka docker-compose this (



```
networks: 
  default: 
    external: 
      name: kafka_confluent_network
```

- And now you have to run all docker-compose and you are ready to use this aplication


```
docker-compose up -d
```


or


```
docker-compose up -d --build
```



## 
Important information about docker

- When running your container, do not forget to inform the network created so that communication between the broker and the consumer is possible.



```
docker run -d --network minha-bridge --name sample nome-da-imagem:tag
```



- When placing this application inside a container the information of the broker address and topic name must be sent via environment variables in docker-compose.
```
environment:
      - BROKER_HOST=broker:29092
      - TOPIC_NAME=br.com.example.correctTopic
```
