import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Browser } from 'protractor';
import { BrowserXhr } from '@angular/http';

@Injectable()
export class ProgressService {

  private uploadProgress: Subject<any>;

  createUploadProgress() {
    this.uploadProgress = new Subject();
    return this.uploadProgress;
  }

  notify(progress) {
    this.uploadProgress.next(progress);
  }
  complete() {
    this.uploadProgress.complete();
  }

  constructor() { }



}


@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr {


  constructor(private service: ProgressService) { super(); }

  build(): XMLHttpRequest {

    var xhr: XMLHttpRequest = super.build();


    xhr.upload.onprogress = (event) => {

      this.service.notify(this.createProgress(event))
    };


    xhr.upload.onloadend = () => {
      this.service.complete();
    }

    return xhr;

  }

  private createProgress(event) {
    return {
      total: event.total,
      percentage: Math.round(event.loaded / event.total * 100)
    };
  }

}
