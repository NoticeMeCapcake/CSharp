using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using WebApp.Models;

namespace WebApp.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly MySqlConnection _dbConnection;
        private readonly IHostEnvironment _hostingEnvironment;

        private static readonly string _selectQuery = $@"select
            id as {nameof(FileTb.Id)},
            filename as {nameof(FileTb.Filename)},
            filepath as {nameof(FileTb.Filepath)},
            lastdate as {nameof(FileTb.LastDate)}
            from filetb;";

        public FileUploadController(IHostEnvironment hosingEnvironment, IConfiguration configuration) {
            _configuration = configuration;
            _hostingEnvironment = hosingEnvironment;
            // _dbConnection = 
            _dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        public IActionResult Post() {
            
            foreach (var file in Request.Form.Files)
            {
                // var upload = file;
                // получаем имя файла
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    // сохраняем файл в папку Files в проекте
                    var filePath =
                            "./userFiles/" + fileName;
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
                        file.CopyTo(fileStream);
                    }
                    var query = @"INSERT INTO filetb (Filename, Filepath, lastdate) VALUES (@Filename, @Filepath, curdate())";
                    _dbConnection.Open();
                    using (var command = new MySqlCommand(query, _dbConnection)) {
                        command.Parameters.AddWithValue("@Filename", fileName);
                        command.Parameters.AddWithValue("@Filepath", filePath);
                        try {
                            command.Prepare();
                            command.ExecuteNonQuery();
                        }
                        catch (MySqlException e) {
                            if (e.Message.Contains("Duplicate")) {
                                query = @"UPDATE filetb SET lastdate = curdate() WHERE filename = @Filename";
                                using (var UPDcommand = new MySqlCommand(query, _dbConnection)) {
                                    UPDcommand.Parameters.AddWithValue("@Filename", fileName);
                                    try {
                                        UPDcommand.Prepare();
                                        UPDcommand.ExecuteNonQuery();
                                    }
                                    catch (MySqlException er) {
                                        _dbConnection.Close();
                                        return BadRequest(new {
                                            Message = "При обновлении файла произошла ошибка"
                                        });
                                    }
                                }
                                    
                                _dbConnection.Close();
                                return Ok(new {
                                    Status = "Success",
                                });
                            }
                            _dbConnection.Close();
                            return BadRequest(new {
                                Status = "Failed",
                                Message = "При создании файла произошла ошибка"
                            });
                        }
                    }
                    _dbConnection.Close();
            }
            return Ok();

        }

        [HttpGet]
        public JsonResult Get() {
            var query = _selectQuery;
            var result = new List<FileTb>();
            // using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                _dbConnection.Open();
                using (var command = new MySqlCommand(query, _dbConnection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            result.Add(new FileTb {
                                Id = reader.GetInt32(0),
                                Filename = reader.GetString(1),
                                Filepath = reader.GetString(2),
                                LastDate = reader.GetDateOnly(3)
                            });
                        }
                    }
                }

                _dbConnection.Close();
            // }

            return new JsonResult(result);

        }
    }
}

