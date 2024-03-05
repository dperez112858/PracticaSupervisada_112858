import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { CondicionIva } from "src/interfaces/condicionIva";

@Injectable()
export class CondicionIvaProvider {
    
    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }
    
    ObtenerCondicionIva(): Observable<CondicionIva[]> {
        return this.http.get<CondicionIva[]>(this.urlApi + 'api/CondicionIva/ObtenerTodas');
    }
}