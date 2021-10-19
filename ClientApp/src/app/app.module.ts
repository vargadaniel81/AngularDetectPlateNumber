import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
//import { CounterComponent } from './counter/counter.component';
//import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { GrayscaleComponent } from './grayscale/grayscale.component';
import { ContrastComponent } from './contrast/contrast.component';
import { RectanglesComponent } from './rectangles/rectangles.component';
import { CropComponent } from './crop/crop.component';
import { SkewComponent } from './skew/skew.component';
import { PlatenumberComponent } from './platenumber/platenumber.component';
import { FileuploadComponent } from './fileupload/fileupload.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    //CounterComponent,
    //FetchDataComponent,
    
    GrayscaleComponent,
    ContrastComponent,
    RectanglesComponent,
    CropComponent,
    SkewComponent,
    PlatenumberComponent,
    FileuploadComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent },
     
      { path: 'grayscale', component: GrayscaleComponent },
      { path: 'contrast', component: ContrastComponent },
      { path: 'rectangles', component: RectanglesComponent },
      { path: 'crop', component: CropComponent },
      { path: 'skew', component: SkewComponent },
      { path: 'platenumber', component: PlatenumberComponent },
      { path: 'fileupload', component: FileuploadComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
