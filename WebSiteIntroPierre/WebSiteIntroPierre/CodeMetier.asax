
<script runat="server">
  Public Class Tty
  {
    Public string Name = "Test";
    Public Tty NextObj = null;
    
    public void Add(Tty obj)
    {
        this.NextObj = obj;
    }  
  }
</script>

<object Class="Tty" Id="Tty1" RunAt="server" Scope="session" />
