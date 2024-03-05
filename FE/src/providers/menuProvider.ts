import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";


@Injectable()
export class MenuProvider {

    constructor(private http: HttpClient) {
    }

    // CotizacionBNA(): Observable<any> {
    //     return this.http.get('https://apis.datos.gob.ar/series/api/series/?ids=168.1_T_CAMBIOR_D_0_0_26&limit=5000&format=json');
    // }
}