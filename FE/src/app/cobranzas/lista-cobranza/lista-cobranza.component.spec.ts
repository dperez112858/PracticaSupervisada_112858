import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaCobranzaComponent } from './lista-cobranza.component';

describe('ListaCobranzaComponent', () => {
  let component: ListaCobranzaComponent;
  let fixture: ComponentFixture<ListaCobranzaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListaCobranzaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaCobranzaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
