<p align="center">
	<img src="https://i.ibb.co/jvnYmKD/image.png" width=250/>
</p>

## Seek4Treasure
Seek4Treasure is a Console Application written in .NET 5.0. It checks all the files in the target folder and captures the sensitive information that exists in it at the end of the day. It can generate the obtained results either on the terminal screen or as output, using JSON format.

## Details
The available arguments are as follows.
| Argument | Explain  |
|--|--|
| lang   |The language to be checked is selected.|
| folder |The path of the folder to be checked is specified. |
| output |Specifies where the Seek4TreasureResult.json file will be created.|

The available languages are as follows.

 - csharp
 - python
 - ruby
 - c
 - all

## Example Usage

```csharp
Seek4Treasure.exe --lang all --folder C:\Users\Desktop\API\ --output C:\Users\Desktop\

Seek4Treasure.exe --lang csharp --folder C:\Users\Desktop\API\
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)