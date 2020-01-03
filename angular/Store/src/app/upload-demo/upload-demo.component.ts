import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-upload-demo',
  templateUrl: './upload-demo.component.html',
  styleUrls: ['./upload-demo.component.css']
})
export class UploadDemoComponent implements OnInit {

  fileData: File = null;
  status: string = "waiting";
  fileUploadedProgress: string = null;
  uploadedFilePath: string = null;

  constructor(private http: HttpClient) { }

  fileProgress(fileInput: any) {
      this.fileData = <File>fileInput.target.files[0];
  }

  ngOnInit() {
  }

  onSubmit() {
      this.status = "start submit";

      const formData = new FormData();
      formData.append('file', this.fileData);

      this.http.post('', formData).subscribe(res => {
          console.log(res);

      }); 

  }

}
