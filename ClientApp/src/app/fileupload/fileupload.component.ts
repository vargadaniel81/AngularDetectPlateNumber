////import { HttpEventType, HttpRequest } from '@angular/common/http';
////import { Component, OnInit } from '@angular/core';

////@Component({
////  selector: 'app-fileupload',
////  templateUrl: './fileupload.component.html',
////  styles: []
////})
////export class FileuploadComponent implements OnInit {
////  progress: number;
////    baseUrl: string;
////    http: any;

////  constructor() { }

////  ngOnInit() {
////  }

  
////}
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-fileupload',
  templateUrl: './fileupload.component.html'
})
export class FileuploadComponent {
  public progress: number;
  public message: string;
  constructor(private http: HttpClient) { }

  upload(files) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (let file of files)
      formData.append(file.name, file);

    const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
    });
  }
}
