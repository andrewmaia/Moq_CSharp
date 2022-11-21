using Moq;
//https://github.com/Moq/moq4/wiki/Quickstart#customizing-mock-behavior

//Modo Strict força que haja um mock para todos os métodos e propriedades
//var mock = new Mock<ITeste>(MockBehavior.Strict); ;
var mock = new Mock<ITeste>(); ;

//Mockando métodos
mock.Setup(teste => teste.FazerAlgo(1)).Returns("Você passou 1");
mock.Setup(teste => teste.FazerAlgo(0)).Throws(new Exception("Não Pode!"));
mock.Setup(teste => teste.FazerAlgo2(It.IsAny<int>())).Returns((int  i)=> i.ToString());

//Mockando propriedades
mock.Setup(teste => teste.Propriedade1).Returns("teste");

//Instanciando classe Mockada
ITeste t = mock.Object;

string retorno1 = t.FazerAlgo(1);
string retorno2 = t.FazerAlgo2(1);
string propriedade1 = t.Propriedade1;
t.Propriedade1 = "teste";

//Irá soltar um exceção
//string retorno2 = t.FazerAlgo(0);

//Os métodos Verify verificam se os métodos/propriedades foram ou não chamados
mock.Verify(teste => teste.FazerAlgo(1));
mock.Verify(teste => teste.FazerAlgo(0),Times.Never);
mock.Verify(teste => teste.FazerAlgo2(1));
mock.VerifyGet(foo => foo.Propriedade1);
mock.VerifySet(teste => teste.Propriedade1 = "teste");

public interface ITeste
{
    string FazerAlgo(int i);
    string FazerAlgo2(int i);
    string Propriedade1 { get; set; }
}
