<<<<<<< HEAD
# 2024TJDBCourseProject
=======
# 2024TJDBCourseProject

## 通信小贴士
*chat说的*
后端接收前端传递的参数方式取决于使用的技术栈和框架，一般可以分为以下几种常见方式：

1. 查询字符串参数
在URL中作为查询字符串传递参数，通常用于GET请求。

示例：

	GET /api/users?name=John&age=30
在ASP.NETCore中，可以通过以下方式接收：

	[HttpGet("api/users")]
	public IActionResult GetUsers(string name, int age)
	{
	    // 使用 name 和 age 参数进行处理
	}
2. 路由参数
在URL路径中传递参数，通常用于RESTful风格的API设计。

示例：
	
	GET /api/users/{id}
在ASP.NETCore中，可以通过以下方式接收：

	[HttpGet("api/users/{id}")]
	public IActionResult GetUserById(int id)
	{
	    // 使用 id 参数进行处理
	}
3. 请求体（Body）参数
通常用于POST、PUT等请求中，参数作为请求体中的JSON或其他格式传递。

示例：

	POST /api/users
	{
	  "name": "John",
	  "age": 30
	}
在ASP.NETCore中，可以通过从请求体中绑定到对象来接收参数：

csharp
复制代码

	[HttpPost("api/users")]
	public IActionResult CreateUser([FromBody] UserDto userDto)
	{
	    // 使用 userDto 对象进行处理
	}
4. 表单数据 **（暂且不用）**
当使用HTML表单提交数据时，参数作为表单字段传递。

示例：

	<form action="/api/users" method="post">
	  <input type="text" name="username" value="John">
	  <input type="number" name="age" value="30">
	  <button type="submit">Submit</button>
	</form>
在ASP.NETCore中，可以通过绑定到表单数据来接收参数：

[HttpPost("api/users")]
public IActionResult CreateUser([FromForm] UserFormData formData)
{
    // 使用 formData 对象进行处理
}
5. 请求头（Headers） **（暂且不用）**
有时参数可以作为HTTP请求头传递，例如授权信息等。

示例：

GET /api/users
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
在ASP.NETCore中，可以通过从请求头中获取值来接收参数：

	[HttpGet("api/users")]
	public IActionResult GetUsers([FromHeader] string authorization)
	{
    // 使用 authorization 参数进行处理
	}

## 要求
前端的诸位在提出api请求文档时，注明**输入**，以上的**输入方式**之一；以及**输出**；以及*可选的***简单**说明即可。简单清楚即可，给自己**省点功夫**。

输出的格式一般会是

	{
		"status":???
		"data":???
	}

有待商榷
一般可以用response.ok检查是不是收到有效的消息
data可能是一个表示条目的字典，可能是一个list，关于输出的类型可以在文档注明
>>>>>>> cb3afef74cc03635ca13361cffb2f04a00b5fdbc
